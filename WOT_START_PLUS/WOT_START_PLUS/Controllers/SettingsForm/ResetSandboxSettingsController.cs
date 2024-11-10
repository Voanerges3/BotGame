

using System;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Configuration;

namespace WOT_START_PLUS.Controllers.Configuration.MainForm
{
    internal sealed class ResetSandboxSettingsController
    {
        private SettingsFormView settingsFormView;
        private SandboxSettingsModel sandboxSettingsModel;
        internal ResetSandboxSettingsController(SettingsFormView settingsFormView, SandboxSettingsModel sandboxSettingsModel)
        {
            this.settingsFormView = settingsFormView;
            this.sandboxSettingsModel = sandboxSettingsModel;


            this.settingsFormView.ResetModel += ResetSandboxSettingsModel;
        }

        private void ResetSandboxSettingsModel(object sender, EventArgs args)
        {
            sandboxSettingsModel.GamesLaunchPath                  = ConstantsUtility.DEFAULT_GAMES_LAUNCH_PATH;
            sandboxSettingsModel.GameWindowsCount                 = ConstantsUtility.DEFAULT_GAME_WINDOWS_COUNT;
            sandboxSettingsModel.WindowTitleToFind                = ConstantsUtility.DEFAULT_WINDOW_TITLE_TO_FIND;
            sandboxSettingsModel.CredentialsPath                  = ConstantsUtility.DEFAULT_CREDENTIALS_PATH;
            sandboxSettingsModel.NewGameWindowTitleBeginning               = ConstantsUtility.DEFAULT_NEW_GAME_WINDOW_TITLE_BEGINNING;
            sandboxSettingsModel.RowCountLadder                   = ConstantsUtility.DEFAULT_ROW_COUNT_LADDER;
            sandboxSettingsModel.ColumnCountLadder                = ConstantsUtility.DEFAULT_COLUMN_COUNT_LADDER;
            sandboxSettingsModel.RowSpacingLadder                 = ConstantsUtility.DEFAULT_ROW_SPACING_LADDER;
            sandboxSettingsModel.ColumnSpacingLadder              = ConstantsUtility.DEFAULT_COLUMN_SPACING_LADDER;

            settingsFormView.UpdateLabels(
                            sandboxSettingsModel.GamesLaunchPath,
                            sandboxSettingsModel.GameWindowsCount,
                            sandboxSettingsModel.WindowTitleToFind,
                             sandboxSettingsModel.CredentialsPath,
                           sandboxSettingsModel.NewGameWindowTitleBeginning,
                              sandboxSettingsModel.RowCountLadder,
                            sandboxSettingsModel.ColumnCountLadder,
                             sandboxSettingsModel.RowSpacingLadder,
                           sandboxSettingsModel.ColumnSpacingLadder);
        }
    }
}
