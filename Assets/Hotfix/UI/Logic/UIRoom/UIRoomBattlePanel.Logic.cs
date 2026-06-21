#if ENABLE_UI_FAIRYGUI
using System.Linq;
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
    public sealed partial class UIRoomBattlePanel
    {
        private string _shownResultKey;
        private bool _resultPopupOpening;

        public override void OnAwake()
        {
            UIGroup = GameApp.UI.GetUIGroup(UIGroupConstants.Window.Name);
            base.OnAwake();
        }

        public override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            GameApp.Event.CheckSubscribe(RoomChangedEventArgs.EventId, OnRoomChanged);

            m_invite_button.self.onClick.Set(CopyInviteText);
            m_leave_button.self.onClick.Set(() => LeaveRoomAndReturnList().Forget());
            m_close_button.self.onClick.Set(CloseSelf);
            m_start_button.self.onClick.Set(() => RoomManager.Instance.StartGame().Forget());
            m_sync_button.self.onClick.Set(OnSyncGameInfoClick);
            m_rock_button.self.onClick.Set(() => RoomManager.Instance.SubmitGesture(RockPaperScissorsGesture.Rock).Forget());
            m_scissors_button.self.onClick.Set(() => RoomManager.Instance.SubmitGesture(RockPaperScissorsGesture.Scissors).Forget());
            m_paper_button.self.onClick.Set(() => RoomManager.Instance.SubmitGesture(RockPaperScissorsGesture.Paper).Forget());
            SetupGestureButtons();

            if (RoomManager.Instance.CurrentRoom == null)
            {
                ReturnList().Forget();
                return;
            }

            Refresh();
            SyncGameInfoIfNeeded().Forget();
        }

        public override void OnClose(bool isShutdown, object userData)
        {
            GameApp.Event.CheckUnsubscribe(RoomChangedEventArgs.EventId, OnRoomChanged);
            base.OnClose(isShutdown, userData);
        }

        private void OnRoomChanged(object sender, GameEventArgs e)
        {
            Refresh();
            TryOpenResultPopup().Forget();
        }

        private void Refresh()
        {
            var manager = RoomManager.Instance;
            var room = manager.CurrentRoom;
            if (room == null)
            {
                return;
            }

            m_room_title_text.text = $"房间 #{room.RoomId}  {GetRoomName(room)}";
            m_room_state_text.text = $"{GetRoomStatusText(room.Status)}  {room.PlayerCount}/{room.MaxPlayerCount}  局数:{room.Round}  房主:{room.OwnerRoleId}";
            m_invite_text.text = manager.InviteText;
            m_result_text.text = GetArenaResultText(room);
            m_arena_tips_text.text = GetArenaTipsText(room);
            m_tips_text.text = GetTipsText(room);

            RefreshPlayer(room.Players.FirstOrDefault(m => m.RoleId == manager.CurrentRoleId), true);
            RefreshPlayer(room.Players.FirstOrDefault(m => m.RoleId != manager.CurrentRoleId), false);

            SetButtonEnabled(m_start_button.self, manager.CanStartGame());
            SetButtonEnabled(m_sync_button.self, IsGameDataStatus(room.Status));
            SetGestureButtonsEnabled(manager.CanSubmitGesture());

            if (room.Status == RoomStatus.Playing)
            {
                _shownResultKey = null;
            }
        }

        private void RefreshPlayer(RoomPlayerInfo player, bool isSelf)
        {
            var avatar = isSelf ? m_self_avatar : m_opponent_avatar;
            var nameText = isSelf ? m_self_name_text : m_opponent_name_text;
            var stateText = isSelf ? m_self_state_text : m_opponent_state_text;
            var gestureText = isSelf ? m_self_gesture_text : m_opponent_gesture_text;

            if (player == null)
            {
                avatar.icon = string.Empty;
                nameText.text = isSelf ? "我" : "等待玩家";
                stateText.text = "空位";
                stateText.color = new Color32(174, 184, 199, 255);
                gestureText.text = "-";
                return;
            }

            var gesturePlayer = RoomManager.Instance.GetRockPaperScissorsPlayer(player.RoleId);
            avatar.icon = player.Avatar > 0 ? UIPackage.GetItemURL(FUIPackage.UICommonAvatar, player.Avatar.ToString()) : string.Empty;
            nameText.text = BuildPlayerName(player, isSelf);
            stateText.text = GetPlayerStateText(player, gesturePlayer);
            stateText.color = GetPlayerStateColor(player, gesturePlayer);
            gestureText.text = GetGestureStateText(gesturePlayer, RoomManager.Instance.CurrentRoom.Status);
        }

        private async UniTask SyncGameInfoIfNeeded()
        {
            var room = RoomManager.Instance.CurrentRoom;
            if (room == null || !IsGameDataStatus(room.Status))
            {
                return;
            }

            await RoomManager.Instance.RequestRockPaperScissorsGameInfo(room.RoomId);
        }

        private async UniTask TryOpenResultPopup()
        {
            var room = RoomManager.Instance.CurrentRoom;
            var game = RoomManager.Instance.CurrentRockPaperScissorsGame;
            if (room == null || game == null || room.Status != RoomStatus.Settled || game.Round != room.Round)
            {
                return;
            }

            var key = room.RoomId + ":" + room.Round;
            if (_resultPopupOpening || _shownResultKey == key)
            {
                return;
            }

            _resultPopupOpening = true;
            try
            {
                _shownResultKey = key;
                await GameApp.UI.OpenAsync<UIRoomResultPopup>(UIGroupConstants.Window);
            }
            finally
            {
                _resultPopupOpening = false;
            }
        }

        private void OnSyncGameInfoClick()
        {
            var room = RoomManager.Instance.CurrentRoom;
            if (room == null)
            {
                return;
            }

            RoomManager.Instance.RequestRockPaperScissorsGameInfo(room.RoomId).Forget();
        }

        private async UniTask LeaveRoomAndReturnList()
        {
            await RoomManager.Instance.LeaveRoom();
            await ReturnList();
        }

        private async UniTask ReturnList()
        {
            GameApp.UI.CloseUIForm<UIRoomBattlePanel>();
            await GameApp.UI.OpenAsync<UIRoomListPanel>(UIGroupConstants.Window);
        }

        private void CloseSelf()
        {
            GameApp.UI.CloseUIForm<UIRoomBattlePanel>();
        }

        private void SetupGestureButtons()
        {
            SetGestureButton(m_rock_button, 0, "石头");
            SetGestureButton(m_scissors_button, 1, "剪刀");
            SetGestureButton(m_paper_button, 2, "布");
        }

        private static void SetGestureButton(UIGestureButton button, int gestureIndex, string title)
        {
            if (button == null)
            {
                return;
            }

            button.m_Gesture.selectedIndex = gestureIndex;
            button.self.title = title;
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

        private static string BuildPlayerName(RoomPlayerInfo player, bool isSelf)
        {
            var name = string.IsNullOrEmpty(player.Name) ? $"玩家{player.RoleId}" : player.Name;
            if (player.IsOwner)
            {
                name += " 房主";
            }

            return isSelf ? "我: " + name : name;
        }

        private static string GetPlayerStateText(RoomPlayerInfo player, RockPaperScissorsPlayerInfo gesturePlayer)
        {
            if (player.OnlineStatus == RoomPlayerOnlineStatus.Reconnecting)
            {
                return "重连中";
            }

            if (player.OnlineStatus == RoomPlayerOnlineStatus.Offline)
            {
                return "离线";
            }

            if (gesturePlayer != null && RoomManager.Instance.CurrentRoom != null && RoomManager.Instance.CurrentRoom.Status == RoomStatus.Playing)
            {
                return gesturePlayer.HasGesture ? "已出拳" : "思考中";
            }

            switch (player.PlayerStatus)
            {
                case RoomPlayerStatus.Idle:
                    return "等待中";
                case RoomPlayerStatus.ReadyInRoom:
                    return "可开始";
                case RoomPlayerStatus.InGame:
                    return "游戏中";
                case RoomPlayerStatus.SettlingInRoom:
                    return "结算中";
                case RoomPlayerStatus.SettledInRoom:
                    return "已结算";
                default:
                    return "在线";
            }
        }

        private static Color GetPlayerStateColor(RoomPlayerInfo player, RockPaperScissorsPlayerInfo gesturePlayer)
        {
            if (player.OnlineStatus != RoomPlayerOnlineStatus.Online)
            {
                return new Color32(238, 198, 112, 255);
            }

            return gesturePlayer != null && gesturePlayer.HasGesture ? new Color32(126, 220, 160, 255) : new Color32(155, 216, 178, 255);
        }

        private static string GetArenaResultText(RoomInfo room)
        {
            if (room.Status == RoomStatus.Settled || room.Status == RoomStatus.Settling)
            {
                return RoomManager.Instance.GetResultText();
            }

            if (room.Status == RoomStatus.Playing)
            {
                return "请选择出拳";
            }

            if (room.Status == RoomStatus.Ready)
            {
                return RoomManager.Instance.IsCurrentPlayerOwner() ? "可以开始游戏" : "等待房主开始";
            }

            return "等待玩家加入";
        }

        private static string GetArenaTipsText(RoomInfo room)
        {
            if (room.Status == RoomStatus.Playing)
            {
                return RoomManager.Instance.HasCurrentPlayerGesture() ? "你已出拳，等待对手。" : "选择石头、剪刀或布。";
            }

            if (room.Status == RoomStatus.Settled)
            {
                return RoomManager.Instance.IsCurrentPlayerOwner() ? "结算已完成，可在弹窗中再来一局。" : "结算已完成，等待房主再开。";
            }

            return "房主开始后，双方选择石头、剪刀或布。";
        }

        private static string GetTipsText(RoomInfo room)
        {
            if (room.Status == RoomStatus.Waiting)
            {
                return "可以复制邀请信息，邀请第二名玩家加入。";
            }

            if (room.Status == RoomStatus.Ready)
            {
                return RoomManager.Instance.IsCurrentPlayerOwner() ? "玩家已就绪，可以开始游戏。" : "玩家已就绪，等待房主开始。";
            }

            return "房间内状态由服务器推送同步。";
        }

        private static string GetGestureStateText(RockPaperScissorsPlayerInfo player, RoomStatus roomStatus)
        {
            if (player == null)
            {
                return "-";
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

        private static bool IsGameDataStatus(RoomStatus status)
        {
            return status == RoomStatus.Playing || status == RoomStatus.Settling || status == RoomStatus.Settled;
        }

        private static void CopyInviteText()
        {
            GUIUtility.systemCopyBuffer = RoomManager.Instance.InviteText;
            Log.Info(RoomManager.Instance.InviteText);
        }
    }
}
#endif
