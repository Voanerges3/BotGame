using AutoItX3Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class WindowHandlesController
    {
        private const int DEFAULT_MISSING_WINDOW_COUNT = 40;

        private readonly MainFormView mainFormView;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly SandboxSettingsModel sandboxSettingsModel;

        private readonly AutoItX3 autoIt;

        internal WindowHandlesController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel, SandboxSettingsModel sandboxSettingsModel)
        {
            this.mainFormView = mainFormView;
            this.gameWindowManagerModel = gameWindowManagerModel;
            this.sandboxSettingsModel = sandboxSettingsModel;
            this.mainFormView.RunUpdateWindowHandles += UpdateWindowHandles;

            autoIt = new AutoItX3();
        }

        private void UpdateWindowHandles(object sender, EventArgs args)
        {

            var newWindowHandles = new Dictionary<int, string>();
            var newWindowHandlesValue = new List<string>();

            var currentIndexWindow = 1;
            var missingWindowCount = 0;

            while (missingWindowCount != DEFAULT_MISSING_WINDOW_COUNT)
            {
                var windowTitle = $"{sandboxSettingsModel.NewGameWindowTitleBeginning}{currentIndexWindow}{sandboxSettingsModel.NewGameWindowTitleEnd}";

                if(autoIt.WinExists(windowTitle, "") == 1)
                {
                    newWindowHandles.Add(currentIndexWindow, windowTitle);
                    newWindowHandlesValue.Add(windowTitle);
                }
                else
                    missingWindowCount++;

                currentIndexWindow++;
            }

            gameWindowManagerModel.WindowHandles = newWindowHandles;
            gameWindowManagerModel.WindowHandlesValue = newWindowHandlesValue;


            MessageBox.Show("Обновление обработчика окон выполнено.");
        }
    }
}
