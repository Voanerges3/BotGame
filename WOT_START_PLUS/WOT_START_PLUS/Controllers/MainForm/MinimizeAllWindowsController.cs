using AutoItX3Lib;
using System;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal class MinimizeAllWindowsController
    {
        private readonly MainFormView mainFormView;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly AutoItX3 autoIt;

        internal MinimizeAllWindowsController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel) 
        {
            this.mainFormView = mainFormView;
            this.gameWindowManagerModel = gameWindowManagerModel;
            this.mainFormView.RunMinimizeAllWindows += MinimizeAllWindows;

            autoIt = new AutoItX3();
        }

        private void MinimizeAllWindows(object sender, EventArgs args)
        {
            foreach (var windowTitle in gameWindowManagerModel.WindowHandlesValue)
                autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
        }
    }
}
