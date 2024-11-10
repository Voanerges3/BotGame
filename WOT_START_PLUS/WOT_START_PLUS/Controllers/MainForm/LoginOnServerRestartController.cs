

using AutoItX3Lib;
using System.Windows.Forms;
using System;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Implementation;
using System.Collections.Generic;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class LoginOnServerRestartController
    {
        private readonly MainFormView mainFormView;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly SandboxSettingsModel sandboxSettingsModel;

        private readonly AutoItX3 autoIt;

        internal LoginOnServerRestartController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel, SandboxSettingsModel sandboxSettingsModel)
        {
            this.mainFormView = mainFormView;
            this.gameWindowManagerModel = gameWindowManagerModel;
            this.sandboxSettingsModel = sandboxSettingsModel;
            autoIt = new AutoItX3();
        }

        private void LoginOnServerRestart(int windowCount, int startIndex, string message)
        {
            if (startIndex < 0 || windowCount < 0 || startIndex + windowCount > gameWindowManagerModel.WindowHandlesValue.Count)
            {
                MessageBox.Show("Обновите Хэндлер");
                return;
            }
            var groupWindows = gameWindowManagerModel.WindowHandlesValue.GetRange(startIndex, windowCount);

            // открываем все окна группы, заходит в аккаунты, сворачиваем, не надо их повторно открывать, лучше сделать как в старте и выходе!
        }

        private void LoadGameWindows(List<int> windowIndexes)
        {
            var totalWindows = 0;

            for (int i = 0; i < sandboxSettingsModel.ExitBattleWindowCountPerAction; i++)
            {

                if (gameWindowManagerModel.WindowHandles.TryGetValue(i, out var windowTitle))
                {
                    autoIt.WinSetState(windowTitle, "", autoIt.SW_RESTORE);
                    totalWindows++;
                }
                else continue;
            }

            //autoIt.Sleep(5000);

            for (int i = totalWindows - sandboxSettingsModel.ExitBattleWindowCountPerAction; i < totalWindows; i++)
            {
                if (gameWindowManagerModel.WindowHandles.TryGetValue(i, out var windowTitle))
                {
                    autoIt.WinSetState(windowTitle, "", autoIt.SW_RESTORE);
                    autoIt.Sleep(1000);
                    autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
                    totalWindows++;
                }
                else continue;
            }
        }
    }
}
