

using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Views.Implementation;
using AutoItX3Lib;
using WOT_START_PLUS.Utilities;
using System;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class CenterAllWindowsController
    {
        private readonly MainFormView mainFormView;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly SandboxSettingsModel sandboxSettingsModel;
        private readonly AutoItX3 autoIt;

        internal CenterAllWindowsController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel, SandboxSettingsModel sandboxSettingsModel)
        {
            this.mainFormView = mainFormView;
            this.gameWindowManagerModel = gameWindowManagerModel;
            this.sandboxSettingsModel = sandboxSettingsModel;
            this.mainFormView.RunCenterAllWindows += CenterAllWindows;

            this.autoIt = new AutoItX3();
        }

        private void CenterAllWindows(object sender, EventArgs args)
        {
            var screenCenterData = HelperUtility.GetScreenCenter(sandboxSettingsModel.WindowGameWidth, sandboxSettingsModel.WindowGameHeight);

            foreach (var windowTitle in gameWindowManagerModel.WindowHandlesValue)
            {
                autoIt.WinActivate(windowTitle);
                autoIt.WinMove(windowTitle, "", screenCenterData.x, screenCenterData.y);
                autoIt.Sleep(200);
                autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
            }
        }
    }
}
