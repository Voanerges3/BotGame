using System;
using System.Diagnostics;
using WOT_START_PLUS.Models.Configuration;
using AutoItX3Lib;
using System.Windows.Forms;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.Configuration
{
    internal sealed class GameLauncherController
    {
        private MainFormView mainFormView;
        private SandboxSettingsModel sandboxSettingsModel;
        private GameWindowManagerModel gameWindowManagerModel;

        private AutoItX3 autoIt;

        public GameLauncherController(MainFormView mainFormView, SandboxSettingsModel sandboxSettingsModel, GameWindowManagerModel gameWindowManagerModel)
        {
            this.mainFormView = mainFormView;
            this.sandboxSettingsModel = sandboxSettingsModel;
            this.gameWindowManagerModel = gameWindowManagerModel;

            this.mainFormView.RunOpenSandbox += RunOpenSandbox;

            autoIt = new AutoItX3();
        }

        private void RunOpenSandbox(object sender, EventArgs args)
        {
            for (int currentIndexWindow = 1; currentIndexWindow < sandboxSettingsModel.GameWindowsCount; currentIndexWindow++)
            {
                Process.Start(sandboxSettingsModel.GamesLaunchPath);

                string newWindowTitle = $"{sandboxSettingsModel.NewGameWindowTitleBeginning}{currentIndexWindow}{sandboxSettingsModel.NewGameWindowTitleEnd}";
                autoIt.WinWait(sandboxSettingsModel.WindowTitleToFind);
                autoIt.WinSetTitle(sandboxSettingsModel.WindowTitleToFind, "", newWindowTitle);
                autoIt.WinSetState(newWindowTitle, "", autoIt.SW_MINIMIZE);

                gameWindowManagerModel.WindowHandles.Add((currentIndexWindow), newWindowTitle);
                gameWindowManagerModel.WindowHandlesValue.Add( newWindowTitle);
            }

            MessageBox.Show("Аккаунты запущены.");
        }
    }
}
