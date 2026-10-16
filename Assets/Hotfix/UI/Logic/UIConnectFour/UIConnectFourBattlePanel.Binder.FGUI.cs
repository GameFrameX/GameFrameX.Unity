#if ENABLE_UI_FAIRYGUI
using System;
using FairyGUI;
using Hotfix.Proto;
using UnityEngine;

namespace Hotfix.UI
{
    public sealed partial class UIConnectFourBattlePanel : IConnectFourBattlePanelBinder
    {
        private static readonly Color EmptyCellColor = new Color32(46, 56, 92, 255);
        private static readonly Color PlayerOneCellColor = new Color32(219, 64, 54, 255);
        private static readonly Color PlayerTwoCellColor = new Color32(242, 191, 38, 255);

        private GGraph[] _cells;
        private GButton[] _columnButtons;

        void IConnectFourBattlePanelBinder.SetRoomTitle(string text)
        {
            m_room_title_text.text = text;
        }

        void IConnectFourBattlePanelBinder.SetRoomState(string text)
        {
            m_room_state_text.text = text;
        }

        void IConnectFourBattlePanelBinder.SetTurnText(string text)
        {
            m_turn_text.text = text;
        }

        void IConnectFourBattlePanelBinder.SetResultText(string text)
        {
            m_result_text.text = text;
        }

        void IConnectFourBattlePanelBinder.SetCell(int displayIndex, ConnectFourCell cell)
        {
            EnsureCellCache();
            if (displayIndex < 0 || displayIndex >= _cells.Length || _cells[displayIndex] == null)
            {
                return;
            }

            _cells[displayIndex].color = GetCellColor(cell);
        }

        void IConnectFourBattlePanelBinder.SetColumnButtonsEnabled(bool enabled)
        {
            EnsureColumnButtonCache();
            foreach (var button in _columnButtons)
            {
                SetButtonEnabled(button, enabled);
            }
        }

        void IConnectFourBattlePanelBinder.SetStartEnabled(bool enabled)
        {
            SetButtonEnabled(m_start_button.self, enabled);
        }

        void IConnectFourBattlePanelBinder.SetRestartEnabled(bool enabled)
        {
            SetButtonEnabled(m_restart_button.self, enabled);
        }

        void IConnectFourBattlePanelBinder.RegisterHandlers(
            Action onLeave,
            Action onClose,
            Action onStart,
            Action onRestart,
            Action<int> onColumn)
        {
            EnsureColumnButtonCache();
            m_leave_button.self.onClick.Set(() => onLeave());
            m_close_button.self.onClick.Set(() => onClose());
            m_start_button.self.onClick.Set(() => onStart());
            m_restart_button.self.onClick.Set(() => onRestart());
            for (var column = 0; column < _columnButtons.Length; column++)
            {
                var capturedColumn = column;
                _columnButtons[column].onClick.Set(() => onColumn(capturedColumn));
            }
        }

        void IConnectFourBattlePanelBinder.ClearHandlers()
        {
            EnsureColumnButtonCache();
            m_leave_button.self.onClick.Clear();
            m_close_button.self.onClick.Clear();
            m_start_button.self.onClick.Clear();
            m_restart_button.self.onClick.Clear();
            foreach (var button in _columnButtons)
            {
                button.onClick.Clear();
            }
        }

        private void EnsureCellCache()
        {
            if (_cells != null)
            {
                return;
            }

            _cells = new GGraph[42];
            for (var displayIndex = 0; displayIndex < _cells.Length; displayIndex++)
            {
                _cells[displayIndex] = (GGraph)GetChild("cell_" + displayIndex);
            }
        }

        private void EnsureColumnButtonCache()
        {
            if (_columnButtons != null)
            {
                return;
            }

            _columnButtons = new GButton[7];
            for (var column = 0; column < _columnButtons.Length; column++)
            {
                _columnButtons[column] = (GButton)GetChild("column_button_" + column);
            }
        }

        private static Color GetCellColor(ConnectFourCell cell)
        {
            switch (cell)
            {
                case ConnectFourCell.PlayerOne:
                    return PlayerOneCellColor;
                case ConnectFourCell.PlayerTwo:
                    return PlayerTwoCellColor;
                default:
                    return EmptyCellColor;
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
