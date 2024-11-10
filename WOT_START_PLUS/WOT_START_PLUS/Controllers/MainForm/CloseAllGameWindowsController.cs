

using AutoItX3Lib;
using System;
using System.Windows.Forms;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class CloseAllGameWindowsController
    {
        private readonly MainFormView mainFormView;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        AutoItX3 autoIt;

        internal CloseAllGameWindowsController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel) 
        {
            this.mainFormView = mainFormView;
            this.gameWindowManagerModel = gameWindowManagerModel;

            this.mainFormView.RunCloseAllGameWindows += CloseAllGameWindows;

            autoIt = new AutoItX3();
        }

        private void CloseAllGameWindows(object sender, EventArgs args)
        {
            if(gameWindowManagerModel.WindowHandlesValue.Count == 0)
                MessageBox.Show("Обновите обработчик окон");


            foreach (var windowTitle in gameWindowManagerModel.WindowHandlesValue)
            {
                if (autoIt.WinExists(windowTitle) == 0) continue;

                //autoIt.WinActivate(windowTitle);
                autoIt.WinClose(windowTitle);
            }
        }
    }
}
