#if ENABLE_UI_FAIRYGUI
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameFrameX.Event.Runtime;
using GameFrameX.UI.Runtime;
using Hotfix.Events;
using Hotfix.Manager;
using Hotfix.Proto;

namespace Hotfix.UI
{
    public sealed partial class UIRoomListPanel
    {
        private const int RoomListRefreshIntervalMilliseconds = 5000;

        private List<object> _roomItems;
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
            m_refresh_button.self.onClick.Set(() => RoomManager.Instance.RequestRoomList().Forget());
            m_create_button.self.onClick.Set(() => CreateRoomAndEnter().Forget());
            m_close_button.self.onClick.Set(CloseSelf);
            m_mask.onClick.Set(CloseSelf);

            Refresh();
            StartRoomListRefreshLoop().Forget();
            RoomManager.Instance.RequestRoomList().Forget();
            EnterBattleIfAlreadyInRoom().Forget();
        }

        public override void OnClose(bool isShutdown, object userData)
        {
            _isOpen = false;
            GameApp.Event.CheckUnsubscribe(RoomChangedEventArgs.EventId, OnRoomChanged);
            base.OnClose(isShutdown, userData);
        }

        private void OnRoomChanged(object sender, GameEventArgs e)
        {
            Refresh();
        }

        private void EnsureCollections()
        {
            if (_roomItems == null)
            {
                _roomItems = new List<object>();
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
            m_summary_text.text = $"当前玩家:{manager.CurrentRoleId}  房间数:{manager.Rooms.Count}";
            m_tips_text.text = manager.CurrentRoom == null ? "加入或创建房间后会进入对战界面。" : "你已在房间中，正在进入对战界面。";

            _roomItems.Clear();
            foreach (var room in manager.Rooms)
            {
                _roomItems.Add(room);
            }

            m_room_list.DataList = _roomItems;
        }

        private void RenderRoomItem(int index, GObject item)
        {
            var room = (RoomInfo)_roomItems[index];
            var uiItem = UIRoomListItem.GetFormPool(item);
            uiItem.m_room_name.text = $"#{room.RoomId} {GetRoomName(room)}";
            uiItem.m_room_status.text = $"{GetRoomStatusText(room.Status)} {room.PlayerCount}/{room.MaxPlayerCount}";
            uiItem.self.selected = room.RoomId == _selectedRoomId;
            uiItem.self.data = room;
            SetButtonEnabled(uiItem.m_join_button.self, CanJoin(room));
            uiItem.m_join_button.self.onClick.Set(() => JoinRoomAndEnter(room.RoomId).Forget());
        }

        private void OnRoomItemClick(EventContext context)
        {
            var uiItem = UIRoomListItem.GetFormPool((GObject)context.data);
            if (uiItem.self.data is RoomInfo room)
            {
                _selectedRoomId = room.RoomId;
                Refresh();
            }
        }

        private async UniTask CreateRoomAndEnter()
        {
            await RoomManager.Instance.CreateRoom(CreateRoomName());
            await EnterBattleIfAlreadyInRoom();
        }

        private async UniTask JoinRoomAndEnter(long roomId)
        {
            await RoomManager.Instance.JoinRoom(roomId);
            await EnterBattleIfAlreadyInRoom();
        }

        private async UniTask EnterBattleIfAlreadyInRoom()
        {
            if (RoomManager.Instance.CurrentRoom == null)
            {
                return;
            }

            GameApp.UI.CloseUIForm<UIRoomListPanel>();
            await GameApp.UI.OpenAsync<UIRoomBattlePanel>(UIGroupConstants.Window);
        }

        private void CloseSelf()
        {
            GameApp.UI.CloseUIForm<UIRoomListPanel>();
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

            if (RoomManager.Instance.CurrentRoom != null)
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

        private static void SetButtonEnabled(GButton button, bool enabled)
        {
            if (button == null)
            {
                return;
            }

            button.enabled = enabled;
            button.alpha = enabled ? 1f : 0.45f;
        }
    }
}
#endif
