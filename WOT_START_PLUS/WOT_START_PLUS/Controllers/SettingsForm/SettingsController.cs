

using System;
using WOT_START_PLUS.Controllers.Navigation;
using WOT_START_PLUS.Enums;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Configuration;

namespace WOT_START_PLUS.Controllers.Configuration
{
    internal sealed class SettingsController
    {
        private NavigationController navigationController;

        private SettingsFormView settingsFormView;
        private SandboxSettingsModel sandboxSettingsModel;

        public SettingsController(SettingsFormView view, SandboxSettingsModel model, NavigationController navigationController)
        {
            this.settingsFormView = view;
            this.sandboxSettingsModel = model;
            this.navigationController = navigationController;

            this.settingsFormView.SaveSettings += ViewSaveSettings;
            this.settingsFormView.GoBackForm += GoToForm;
            UpdateView();
        }
        // WindowTitleGame MaxIndexSearchRangeForGameWindow
        private void ViewSaveSettings(object sender, EventArgs e)
        {
            // String:
            if (settingsFormView.GamesLaunchPath != string.Empty)
                sandboxSettingsModel.GamesLaunchPath = settingsFormView.GamesLaunchPath;

            if (settingsFormView.WindowTitleToFind != string.Empty)
                sandboxSettingsModel.WindowTitleToFind = settingsFormView.WindowTitleToFind;

            if (settingsFormView.CredentialsPath != string.Empty)
                sandboxSettingsModel.CredentialsPath = settingsFormView.CredentialsPath;

            if (settingsFormView.NewGameWindowTitle != string.Empty)
                sandboxSettingsModel.NewGameWindowTitleBeginning = settingsFormView.NewGameWindowTitle;


            // Int:
            sandboxSettingsModel.GameWindowsCount = settingsFormView.GameWindowsCount;
            sandboxSettingsModel.RowCountLadder = settingsFormView.RowCountLadder;
            sandboxSettingsModel.ColumnCountLadder = settingsFormView.ColumnCountLadder;
            sandboxSettingsModel.RowSpacingLadder = settingsFormView.RowSpacingLadder;
            sandboxSettingsModel.ColumnSpacingLadder = settingsFormView.ColumnSpacingLadder;

            UpdateView();
        }

        private void UpdateView()
        {
            settingsFormView.UpdateLabels(sandboxSettingsModel.GamesLaunchPath,
                                         sandboxSettingsModel.GameWindowsCount,
                                        sandboxSettingsModel.WindowTitleToFind,
                                         sandboxSettingsModel.CredentialsPath,
                                       sandboxSettingsModel.NewGameWindowTitleBeginning,
                                          sandboxSettingsModel.RowCountLadder,
                                        sandboxSettingsModel.ColumnCountLadder,
                                         sandboxSettingsModel.RowSpacingLadder,
                                       sandboxSettingsModel.ColumnSpacingLadder);
            settingsFormView.ResetTextBoxes();
        }
        // // windowTitleToFind credentialsPath newGameWindowTitle
        private void GoToForm(object sender, EventArgs e) => navigationController.ShowForm(ConstantsUtility.STARTUP_FORM);
    }
}
