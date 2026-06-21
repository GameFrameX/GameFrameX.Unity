using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameFrameX.Network.Runtime;
using GameFrameX.Runtime;
using Hotfix.Events;
using Hotfix.Proto;

namespace Hotfix.Manager
{
    /// <summary>
    /// 通用房间与石头剪刀布玩法客户端状态管理。
    /// </summary>
    public sealed class RoomManager : GameFrameworkSingleton<RoomManager>, IMessageHandler
    {
        private readonly List<RoomInfo> _rooms = new List<RoomInfo>();

        public RoomInfo CurrentRoom { get; private set; }

        public RockPaperScissorsGameInfo CurrentRockPaperScissorsGame { get; private set; }

        public IReadOnlyList<RoomInfo> Rooms
        {
            get { return _rooms; }
        }

        public string InviteText
        {
            get
            {
                return CurrentRoom == null ? string.Empty : $"邀请你加入房间 {CurrentRoom.RoomId}：{CurrentRoom.Name}";
            }
        }

        public long CurrentRoleId
        {
            get
            {
                return PlayerManager.Instance.PlayerInfo == null ? 0 : PlayerManager.Instance.PlayerInfo.Id;
            }
        }

        public RoomManager()
        {
            Register();
        }

        public void Register()
        {
            ProtoMessageHandler.Add(this);
        }

        [MessageHandler(typeof(NotifyRoomChanged), nameof(NotifyRoomChanged))]
        private void NotifyRoomChanged(NotifyRoomChanged msg)
        {
            UpsertRoom(msg.Room);
            if (CurrentRoom != null && msg.Room != null && CurrentRoom.RoomId == msg.Room.RoomId)
            {
                CurrentRoom = msg.Room;
                SynchronizeCurrentGameInfo().Forget();
            }

            FireChanged();
        }

        [MessageHandler(typeof(NotifyRockPaperScissorsGameChanged), nameof(NotifyRockPaperScissorsGameChanged))]
        private void NotifyRockPaperScissorsGameChanged(NotifyRockPaperScissorsGameChanged msg)
        {
            CurrentRockPaperScissorsGame = msg.GameInfo;
            FireChanged();
        }

        public async UniTask RequestRoomList(GameType gameType = GameType.RockPaperScissors)
        {
            var response = await NetworkCall<RespRoomList>(new ReqRoomList
            {
                GameType = gameType,
                IncludeClosed = false
            });
            if (response.ErrorCode != default)
            {
                Log.Warning($"请求房间列表失败:{response.ErrorCode}");
                return;
            }

            _rooms.Clear();
            _rooms.AddRange(response.Rooms);
            FireChanged();
        }

        public async UniTask CreateRoom(string roomName = "")
        {
            var response = await NetworkCall<RespCreateRoom>(new ReqCreateRoom
            {
                GameType = GameType.RockPaperScissors,
                Name = roomName,
                MinPlayerCount = 2,
                MaxPlayerCount = 2
            });
            if (response.ErrorCode != default)
            {
                Log.Warning($"创建房间失败:{response.ErrorCode}");
                return;
            }

            CurrentRoom = response.Room;
            CurrentRockPaperScissorsGame = null;
            UpsertRoom(response.Room);
            FireChanged();
        }

        public async UniTask JoinRoom(long roomId)
        {
            var response = await NetworkCall<RespJoinRoom>(new ReqJoinRoom { RoomId = roomId });
            if (response.ErrorCode != default)
            {
                Log.Warning($"加入房间失败:{response.ErrorCode}");
                return;
            }

            CurrentRoom = response.Room;
            UpsertRoom(response.Room);
            await RequestRockPaperScissorsGameInfo(roomId);
            FireChanged();
        }

        public async UniTask LeaveRoom()
        {
            if (CurrentRoom == null)
            {
                return;
            }

            var response = await NetworkCall<RespLeaveRoom>(new ReqLeaveRoom { RoomId = CurrentRoom.RoomId });
            if (response.ErrorCode != default)
            {
                Log.Warning($"退出房间失败:{response.ErrorCode}");
                return;
            }

            UpsertRoom(response.Room);
            CurrentRoom = null;
            CurrentRockPaperScissorsGame = null;
            FireChanged();
        }

        public async UniTask StartGame()
        {
            if (CurrentRoom == null)
            {
                return;
            }

            var response = await NetworkCall<RespStartRoomGame>(new ReqStartRoomGame { RoomId = CurrentRoom.RoomId });
            if (response.ErrorCode != default)
            {
                Log.Warning($"开始游戏失败:{response.ErrorCode}");
                return;
            }

            CurrentRoom = response.Room;
            UpsertRoom(response.Room);
            await RequestRockPaperScissorsGameInfo(response.Room.RoomId);
            FireChanged();
        }

        public async UniTask SubmitGesture(RockPaperScissorsGesture gesture)
        {
            if (CurrentRoom == null)
            {
                return;
            }

            var response = await NetworkCall<RespSubmitRockPaperScissorsGesture>(new ReqSubmitRockPaperScissorsGesture
            {
                RoomId = CurrentRoom.RoomId,
                Gesture = gesture
            });
            if (response.ErrorCode != default)
            {
                Log.Warning($"出拳失败:{response.ErrorCode}");
                return;
            }

            CurrentRockPaperScissorsGame = response.GameInfo;
            FireChanged();
        }

        public async UniTask RestartGame()
        {
            if (CurrentRoom == null)
            {
                return;
            }

            var response = await NetworkCall<RespRestartRockPaperScissorsGame>(new ReqRestartRockPaperScissorsGame { RoomId = CurrentRoom.RoomId });
            if (response.ErrorCode != default)
            {
                Log.Warning($"再来一局失败:{response.ErrorCode}");
                return;
            }

            CurrentRockPaperScissorsGame = response.GameInfo;
            FireChanged();
        }

        public async UniTask RequestRockPaperScissorsGameInfo(long roomId)
        {
            var response = await NetworkCall<RespRockPaperScissorsGameInfo>(new ReqRockPaperScissorsGameInfo { RoomId = roomId });
            if (response.ErrorCode != default)
            {
                Log.Warning($"请求猜拳信息失败:{response.ErrorCode}");
                return;
            }

            CurrentRockPaperScissorsGame = response.GameInfo;
            FireChanged();
        }

        private async UniTask SynchronizeCurrentGameInfo()
        {
            if (CurrentRoom == null)
            {
                CurrentRockPaperScissorsGame = null;
                return;
            }

            if (!IsGameDataStatus(CurrentRoom.Status))
            {
                CurrentRockPaperScissorsGame = null;
                return;
            }

            await RequestRockPaperScissorsGameInfo(CurrentRoom.RoomId);
        }

        public bool IsCurrentPlayerOwner()
        {
            return CurrentRoom != null && CurrentRoom.OwnerRoleId == CurrentRoleId;
        }

        public bool HasCurrentPlayerGesture()
        {
            return HasGesture(CurrentRoleId);
        }

        public bool HasGesture(long roleId)
        {
            if (CurrentRockPaperScissorsGame == null)
            {
                return false;
            }

            var playerInfo = CurrentRockPaperScissorsGame.Players.FirstOrDefault(m => m.RoleId == roleId);
            return playerInfo != null && playerInfo.HasGesture;
        }

        public RockPaperScissorsPlayerInfo GetRockPaperScissorsPlayer(long roleId)
        {
            return CurrentRockPaperScissorsGame == null ? null : CurrentRockPaperScissorsGame.Players.FirstOrDefault(m => m.RoleId == roleId);
        }

        public RockPaperScissorsPlayerInfo GetCurrentRockPaperScissorsPlayer()
        {
            return GetRockPaperScissorsPlayer(CurrentRoleId);
        }

        public RockPaperScissorsPlayerInfo GetOpponentRockPaperScissorsPlayer()
        {
            if (CurrentRockPaperScissorsGame == null)
            {
                return null;
            }

            var roleId = CurrentRoleId;
            return CurrentRockPaperScissorsGame.Players.FirstOrDefault(m => m.RoleId != roleId);
        }

        public RoomPlayerInfo GetOpponentRoomPlayer()
        {
            if (CurrentRoom == null)
            {
                return null;
            }

            var roleId = CurrentRoleId;
            return CurrentRoom.Players.FirstOrDefault(m => m.RoleId != roleId);
        }

        public bool CanStartGame()
        {
            return CurrentRoom != null && IsCurrentPlayerOwner() && CurrentRoom.Status == RoomStatus.Ready;
        }

        public bool CanSubmitGesture()
        {
            return CurrentRoom != null && CurrentRoom.Status == RoomStatus.Playing && !HasCurrentPlayerGesture();
        }

        public bool CanRestartGame()
        {
            return CurrentRoom != null && IsCurrentPlayerOwner() && CurrentRoom.Status == RoomStatus.Settled;
        }

        public string GetResultText()
        {
            if (CurrentRoom == null)
            {
                return "未进入房间";
            }

            if (CurrentRoom.Status != RoomStatus.Settling && CurrentRoom.Status != RoomStatus.Settled)
            {
                return "等待双方出拳";
            }

            if (CurrentRockPaperScissorsGame == null)
            {
                return "等待结算数据";
            }

            if (CurrentRockPaperScissorsGame.WinnerRoleId == 0)
            {
                return "平局";
            }

            return CurrentRockPaperScissorsGame.WinnerRoleId == CurrentRoleId ? "你赢了" : "你输了";
        }

        private static async UniTask<TResponse> NetworkCall<TResponse>(MessageObject request) where TResponse : MessageObject, IResponseMessage
        {
            return await GameApp.Network.GetNetworkChannel("network").Call<TResponse>(request);
        }

        private void UpsertRoom(RoomInfo room)
        {
            if (room == null)
            {
                return;
            }

            var index = _rooms.FindIndex(m => m.RoomId == room.RoomId);
            if (index >= 0)
            {
                _rooms[index] = room;
                return;
            }

            _rooms.Add(room);
        }

        private static bool IsGameDataStatus(RoomStatus status)
        {
            return status == RoomStatus.Playing || status == RoomStatus.Settling || status == RoomStatus.Settled;
        }

        private static void FireChanged()
        {
            GameApp.Event.Fire(RoomManager.Instance, RoomChangedEventArgs.Create());
        }
    }
}
