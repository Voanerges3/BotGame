

using System;
using System.Windows.Forms;
using WOT_START_PLUS.Controllers.Navigation;
using WOT_START_PLUS.Enums;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.Configuration.MainForm
{
    internal sealed class MainFormController
    {
        private NavigationController navigationController;

        private MainFormView view;
        private SandboxSettingsModel model;

        public MainFormController(MainFormView view, NavigationController navigationController)
        {
            this.view = view;
            this.navigationController = navigationController;

            this.view.OpenSettings += OpenSettings;
            this.view.OpenDebug += OpenDebug;
            UpdateView(); // полоска загрузки и время до окончания
        }

        private void OpenSettings(object sender, EventArgs e) => navigationController.ShowForm(FormType.SettingsForm);
        private void OpenDebug(object sender, EventArgs e) => navigationController.ShowForm(FormType.DebugCountBattleAccountsForm);

        private void UpdateView()
        {
            
        }
    }
}
