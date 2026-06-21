#if ENABLE_UI_FAIRYGUI
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameFrameX.UI.Runtime;
using Hotfix.Manager;
using Hotfix.Proto;

namespace Hotfix.UI
{
    public sealed partial class UIRoomResultPopup
    {
        public override void OnAwake()
        {
            UIGroup = GameApp.UI.GetUIGroup(UIGroupConstants.Window.Name);
            base.OnAwake();
        }

        public override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_close_button.self.onClick.Set(CloseSelf);
            m_mask.onClick.Set(CloseSelf);
            m_restart_button.self.onClick.Set(() => RestartAndClose().Forget());

            Refresh();
        }

        private void Refresh()
        {
            var manager = RoomManager.Instance;
            var room = manager.CurrentRoom;
            var game = manager.CurrentRockPaperScissorsGame;
            m_result_text.text = manager.GetResultText();
            m_round_text.text = room == null ? "第 0 局" : $"第 {room.Round} 局";

            var selfPlayer = room?.Players.FirstOrDefault(m => m.RoleId == manager.CurrentRoleId);
            var opponentPlayer = room?.Players.FirstOrDefault(m => m.RoleId != manager.CurrentRoleId);
            var selfGesture = game?.Players.FirstOrDefault(m => m.RoleId == manager.CurrentRoleId);
            var opponentGesture = game?.Players.FirstOrDefault(m => m.RoleId != manager.CurrentRoleId);

            m_self_name_text.text = selfPlayer == null ? "我" : GetPlayerName(selfPlayer, true);
            m_opponent_name_text.text = opponentPlayer == null ? "对手" : GetPlayerName(opponentPlayer, false);
            m_self_gesture_text.text = GetGestureText(selfGesture == null ? RockPaperScissorsGesture.None : selfGesture.Gesture);
            m_opponent_gesture_text.text = GetGestureText(opponentGesture == null ? RockPaperScissorsGesture.None : opponentGesture.Gesture);
            SetButtonEnabled(m_restart_button.self, manager.CanRestartGame());
        }

        private async UniTask RestartAndClose()
        {
            if (!RoomManager.Instance.CanRestartGame())
            {
                return;
            }

            await RoomManager.Instance.RestartGame();
            CloseSelf();
        }

        private void CloseSelf()
        {
            GameApp.UI.CloseUIForm<UIRoomResultPopup>();
        }

        private static string GetPlayerName(RoomPlayerInfo player, bool isSelf)
        {
            var name = string.IsNullOrEmpty(player.Name) ? $"玩家{player.RoleId}" : player.Name;
            return isSelf ? "我: " + name : name;
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
