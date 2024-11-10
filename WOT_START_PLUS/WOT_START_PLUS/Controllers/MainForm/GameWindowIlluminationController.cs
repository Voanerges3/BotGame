// 1048704

using System;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class GameWindowIlluminationController
    {
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly MainFormView mainFormView;

        internal GameWindowIlluminationController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel)
        {
            this.gameWindowManagerModel = gameWindowManagerModel;
            this.mainFormView = mainFormView;

            this.mainFormView.IlluminationRequested += HandleIlluminationRequest;
        }

        private void HandleIlluminationRequest(object sender, EventArgs args)
        {
            //MessageBox.Show();
            //mainFormView.DisplayIlluminatedCount(illuminatedCount);
        }
    }
}
