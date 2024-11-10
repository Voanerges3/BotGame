

using System;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.Configuration.MainForm
{
    internal sealed class TaskbarGameController
    {
        private MainFormView mainFormView;

        private GameWindowManagerModel gameWindowManagerModel;

        internal TaskbarGameController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel)
        {
            this.mainFormView = mainFormView;
            this.gameWindowManagerModel = gameWindowManagerModel;

            this.mainFormView.RunSortGameWindowsOnTaskbar += SortGameWindowsOnTaskbar;
        }
        
        private void SortGameWindowsOnTaskbar(object sender, EventArgs args)
        {
            // открываем каждое окно, затем сворачиваем, после перебираем по названию?
        }
    }
}
