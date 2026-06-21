#if ENABLE_UI_FAIRYGUI
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameFrameX.Event.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using Hotfix.Events;
using Hotfix.Manager;
using Hotfix.Proto;
using UnityEngine;

namespace Hotfix.UI
{
    public sealed partial class UIRoomPanel
    {
        private const int RoomListRefreshIntervalMilliseconds = 5000;

        private List<object> _roomItems;
        private List<object> _playerItems;
        private long _selectedRoomId;
        private bool _isOpen;
        private bool _roomListRefreshLoopRunning;

        public override void OnAwake()
        {
            EnsureCollections();
            UIGroup = GameApp.UI.GetUIGroup(UIGroupConstants.Window.Name);
            base.OnAwake();
        }

        public override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            EnsureCollections();
            _isOpen = true;
            GameApp.Event.CheckSubscribe(RoomChangedEventArgs.EventId, OnRoomChanged);

            m_room_list.itemRenderer = RenderRoomItem;
            m_room_list.onClickItem.Set(OnRoomItemClick);
            m_player_list.itemRenderer = RenderPlayerItem;

            m_refresh_button.self.onClick.Set(() => RoomManager.Instance.RequestRoomList().Forget());
            m_create_button.self.onClick.Set(() => RoomManager.Instance.CreateRoom(CreateRoomName()).Forget());
            m_close_button.self.onClick.Set(OnCloseClick);
            m_mask.onClick.Set(OnCloseClick);
            m_start_button.self.onClick.Set(() => RoomManager.Instance.StartGame().Forget());
            m_leave_button.self.onClick.Set(() => RoomManager.Instance.LeaveRoom().Forget());
            m_invite_button.self.onClick.Set(CopyInviteText);
            m_sync_button.self.onClick.Set(OnSyncGameInfoClick);
            m_restart_button.self.onClick.Set(() => RoomManager.Instance.RestartGame().Forget());
            m_rock_button.self.onClick.Set(() => RoomManager.Instance.SubmitGesture(RockPaperScissorsGesture.Rock).Forget());
            m_scissors_button.self.onClick.Set(() => RoomManager.Instance.SubmitGesture(RockPaperScissorsGesture.Scissors).Forget());
            m_paper_button.self.onClick.Set(() => RoomManager.Instance.SubmitGesture(RockPaperScissorsGesture.Paper).Forget());

            SetupGestureButtons();
            Refresh();
            StartRoomListRefreshLoop().Forget();
            RoomManager.Instance.RequestRoomList().Forget();
        }

        public override void OnClose(bool isShutdown, object userData)
        {
            _isOpen = false;
            GameApp.Event.CheckUnsubscribe(RoomChangedEventArgs.EventId, OnRoomChanged);
            base.OnClose(isShutdown, userData);
        }

        private void OnRoomChanged(object sender, GameEventArgs e)
        {
            EnsureCollections();
            Refresh();
        }

        private void EnsureCollections()
        {
            if (_roomItems == null)
            {
                _roomItems = new List<object>();
            }

            if (_playerItems == null)
            {
                _playerItems = new List<object>();
            }
        }

        private async UniTask StartRoomListRefreshLoop()
        {
            if (_roomListRefreshLoopRunning)
            {
                return;
            }

            _roomListRefreshLoopRunning = true;
            try
            {
                while (_isOpen && !IsDisposed)
                {
                    await UniTask.Delay(RoomListRefreshIntervalMilliseconds);
                    if (!_isOpen || IsDisposed)
                    {
                        break;
                    }

                    await RoomManager.Instance.RequestRoomList();
                }
            }
            finally
            {
                _roomListRefreshLoopRunning = false;
            }
        }

        private void Refresh()
        {
            var manager = RoomManager.Instance;
            if (manager.CurrentRoom != null)
            {
                _selectedRoomId = manager.CurrentRoom.RoomId;
            }

            m_summary_text.text = $"当前玩家:{manager.CurrentRoleId}  房间数:{manager.Rooms.Count}";
            m_tips_text.text = GetTips();
            RefreshRooms();
            RefreshRoomDetail();
            RefreshPlayers();
            RefreshGame();
        }

        private void RefreshRooms()
        {
            _roomItems.Clear();
            foreach (var room in RoomManager.Instance.Rooms)
            {
                _roomItems.Add(room);
            }

            m_room_list.DataList = _roomItems;
        }

        private void RefreshRoomDetail()
        {
            var room = RoomManager.Instance.CurrentRoom;
            if (room == null)
            {
                m_room_detail_text.text = "尚未进入房间。\n可以从列表加入，或创建一个新房间。";
                m_invite_text.text = string.Empty;
                SetButtonEnabled(m_start_button.self, false);
                SetButtonEnabled(m_leave_button.self, false);
                SetButtonEnabled(m_invite_button.self, false);
                SetButtonEnabled(m_sync_button.self, false);
                SetButtonEnabled(m_restart_button.self, false);
                return;
            }

            m_room_detail_text.text = $"房间ID:{room.RoomId}\n名称:{GetRoomName(room)}\n状态:{GetRoomStatusText(room.Status)}\n人数:{room.PlayerCount}/{room.MaxPlayerCount}\n局数:{room.Round}\n房主:{room.OwnerRoleId}";
            m_invite_text.text = RoomManager.Instance.InviteText;
            SetButtonEnabled(m_start_button.self, RoomManager.Instance.CanStartGame());
            SetButtonEnabled(m_leave_button.self, true);
            SetButtonEnabled(m_invite_button.self, true);
            SetButtonEnabled(m_sync_button.self, room.Status != RoomStatus.Waiting && room.Status != RoomStatus.Ready);
            SetButtonEnabled(m_restart_button.self, RoomManager.Instance.CanRestartGame());
        }

        private void RefreshPlayers()
        {
            _playerItems.Clear();
            var room = RoomManager.Instance.CurrentRoom;
            if (room != null)
            {
                for (var i = 0; i < room.MaxPlayerCount; i++)
                {
                    _playerItems.Add(i < room.Players.Count ? room.Players[i] : null);
                }
            }

            m_player_list.DataList = _playerItems;
        }

        private void RefreshGame()
        {
            var room = RoomManager.Instance.CurrentRoom;
            if (room == null)
            {
                m_result_text.text = "未进入房间";
                m_self_gesture_text.text = "你: -";
                m_opponent_gesture_text.text = "对手: -";
                SetGestureButtonsEnabled(false);
                return;
            }

            m_result_text.text = RoomManager.Instance.GetResultText();
            m_self_gesture_text.text = "你: " + GetGestureStateText(RoomManager.Instance.GetCurrentRockPaperScissorsPlayer(), room.Status);
            m_opponent_gesture_text.text = "对手: " + GetGestureStateText(RoomManager.Instance.GetOpponentRockPaperScissorsPlayer(), room.Status);
            SetGestureButtonsEnabled(RoomManager.Instance.CanSubmitGesture());
        }

        private void RenderRoomItem(int index, GObject item)
        {
            var room = (RoomInfo)_roomItems[index];
            var uiItem = UIRoomListItem.GetFormPool(item);
            uiItem.m_room_name.text = $"#{room.RoomId} {GetRoomName(room)}";
            uiItem.m_room_status.text = $"{GetRoomStatusText(room.Status)} {room.PlayerCount}/{room.MaxPlayerCount}";
            uiItem.self.selected = room.RoomId == _selectedRoomId;
            SetButtonEnabled(uiItem.m_join_button.self, CanJoin(room));
            uiItem.m_join_button.self.onClick.Set(() => RoomManager.Instance.JoinRoom(room.RoomId).Forget());
        }

        private void RenderPlayerItem(int index, GObject item)
        {
            var player = _playerItems[index] as RoomPlayerInfo;
            var uiItem = UIRoomPlayerItem.GetFormPool(item);
            if (player == null)
            {
                uiItem.m_role_text.text = "等待玩家";
                uiItem.m_seat_text.text = $"座位 {index + 1}";
                uiItem.m_state_text.text = "空位";
                uiItem.m_state_text.color = new Color32(174, 184, 199, 255);
                return;
            }

            var label = player.RoleId == RoomManager.Instance.CurrentRoleId ? "我" : "对手";
            var gesturePlayer = RoomManager.Instance.GetRockPaperScissorsPlayer(player.RoleId);
            uiItem.m_role_text.text = $"{label}: {player.RoleId}";
            uiItem.m_seat_text.text = player.IsOwner ? $"座位 {index + 1} 房主" : $"座位 {index + 1}";
            uiItem.m_state_text.text = gesturePlayer == null ? "未开局" : gesturePlayer.HasGesture ? "已出拳" : "思考中";
            uiItem.m_state_text.color = GetGestureStateColor(gesturePlayer);
        }

        private void OnRoomItemClick(EventContext context)
        {
            var uiItem = UIRoomListItem.GetFormPool((GObject)context.data);
            if (uiItem.self.dataSource is RoomInfo room)
            {
                _selectedRoomId = room.RoomId;
                RefreshRooms();
            }
        }

        private void OnSyncGameInfoClick()
        {
            if (RoomManager.Instance.CurrentRoom == null)
            {
                return;
            }

            RoomManager.Instance.RequestRockPaperScissorsGameInfo(RoomManager.Instance.CurrentRoom.RoomId).Forget();
        }

        private void OnCloseClick()
        {
            GameApp.UI.CloseUIForm<UIRoomPanel>();
        }

        private static void SetupGestureButtons()
        {
            // The exported FairyGUI component owns the visual states. Logic only sets labels/controllers.
        }

        private void SetGestureButtonsEnabled(bool enabled)
        {
            SetButtonEnabled(m_rock_button.self, enabled);
            SetButtonEnabled(m_scissors_button.self, enabled);
            SetButtonEnabled(m_paper_button.self, enabled);
        }

        private static void SetButtonEnabled(GButton button, bool enabled)
        {
            if (button == null)
            {
                return;
            }

            button.enabled = enabled;
            button.alpha = enabled ? 1f : 0.45f;
        }

        private static string CreateRoomName()
        {
            var player = PlayerManager.Instance.PlayerInfo;
            return player == null || string.IsNullOrEmpty(player.Name) ? "猜拳房间" : $"{player.Name}的猜拳房间";
        }

        private static bool CanJoin(RoomInfo room)
        {
            if (room == null)
            {
                return false;
            }

            if (RoomManager.Instance.CurrentRoom != null && RoomManager.Instance.CurrentRoom.RoomId == room.RoomId)
            {
                return false;
            }

            return (room.Status == RoomStatus.Waiting || room.Status == RoomStatus.Ready) && room.PlayerCount < room.MaxPlayerCount;
        }

        private static string GetRoomName(RoomInfo room)
        {
            return room == null || string.IsNullOrEmpty(room.Name) ? "猜拳房间" : room.Name;
        }

        private static string GetRoomStatusText(RoomStatus status)
        {
            switch (status)
            {
                case RoomStatus.Waiting:
                    return "等待中";
                case RoomStatus.Ready:
                    return "可开始";
                case RoomStatus.Playing:
                    return "游戏中";
                case RoomStatus.Settling:
                    return "结算中";
                case RoomStatus.Settled:
                    return "已结算";
                case RoomStatus.Closed:
                    return "已关闭";
                case RoomStatus.Disbanded:
                    return "已解散";
                default:
                    return "未知";
            }
        }

        private static string GetGestureStateText(RockPaperScissorsPlayerInfo player, RoomStatus roomStatus)
        {
            if (player == null)
            {
                return "未开始";
            }

            if (roomStatus == RoomStatus.Settling || roomStatus == RoomStatus.Settled)
            {
                return GetGestureText(player.Gesture);
            }

            return player.HasGesture ? "已出拳" : "未出拳";
        }

        private static string GetGestureText(RockPaperScissorsGesture gesture)
        {
            switch (gesture)
            {
                case RockPaperScissorsGesture.Rock:
                    return "石头";
                case RockPaperScissorsGesture.Scissors:
                    return "剪刀";
                case RockPaperScissorsGesture.Paper:
                    return "布";
                default:
                    return "无";
            }
        }

        private static Color GetGestureStateColor(RockPaperScissorsPlayerInfo player)
        {
            if (player == null)
            {
                return new Color32(150, 160, 176, 255);
            }

            return player.HasGesture ? new Color32(126, 220, 160, 255) : new Color32(238, 198, 112, 255);
        }

        private static string GetTips()
        {
            var room = RoomManager.Instance.CurrentRoom;
            if (room == null)
            {
                return "房间流程: 创建或加入房间 -> 房主开始 -> 双方出拳 -> 自动结算。";
            }

            if (room.Status == RoomStatus.Ready && RoomManager.Instance.IsCurrentPlayerOwner())
            {
                return "玩家已满，可以开始游戏。";
            }

            if (room.Status == RoomStatus.Playing)
            {
                return RoomManager.Instance.HasCurrentPlayerGesture() ? "你已出拳，等待对手。" : "请选择石头、剪刀或布。";
            }

            if (room.Status == RoomStatus.Settled)
            {
                return RoomManager.Instance.IsCurrentPlayerOwner() ? "本局已结算，可以再来一局。" : "本局已结算，等待房主再开。";
            }

            return "可以邀请第二名玩家加入房间。";
        }

        private static void CopyInviteText()
        {
            GUIUtility.systemCopyBuffer = RoomManager.Instance.InviteText;
            Log.Info(RoomManager.Instance.InviteText);
        }
    }
}
#endif
