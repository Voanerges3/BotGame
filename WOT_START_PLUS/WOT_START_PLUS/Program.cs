using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using WOT_START_PLUS.Controllers.Configuration;
using WOT_START_PLUS.Controllers.Configuration.MainForm;
using WOT_START_PLUS.Controllers.MainForm;
using WOT_START_PLUS.Controllers.Navigation;
using WOT_START_PLUS.Enums;
using WOT_START_PLUS.Models;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Configuration;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS
{
    internal static class Program
    {
        private static SandboxSettingsModel sandboxSettingsModel;
        private static GameWindowGroupModel gameWindowGroupModel;
        private static GameWindowManagerModel gameWindowManagerModel;
        private static BattleAccountCountModel battleAccountCountModel;

        private static NavigationController navigationController;
        private static SettingsController settingsController; 
        private static GameLauncherController gameLauncherController;
        private static BattleStarterController gameWindowManagerController;
        private static MainFormController mainFormController;
        private static TaskbarGameController taskbarGameController;
        private static ResetSandboxSettingsController resetSandboxSettingsController;
        private static GameWindowIlluminationController gameWindowIlluminationController;
        private static AutoAuthController autoAuthController;
        private static AutoWindowLadderController autoWindowLadderController;
        private static BattleExitController battleExitController;
        private static WindowHandlesController windowHandlesController;
        private static CloseAllGameWindowsController closeAllGameWindowsController;
        private static CenterAllWindowsController centerAllWindowsController;
        private static CountBattleAccountsController countBattleAccountsController;
        private static ResetDisplaysCountBattleAccountsController resetDisplaysCountBattleAccountsController;
        private static MinimizeAllWindowsController minimizeAllWindowsController;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitializeComponents();

            Application.Run(navigationController.Forms[ConstantsUtility.STARTUP_FORM]);
            //Application.Run(new MainFormView());
        }

        private static void InitializeComponents()
        {
            InitializeModel();
            InitializeController();
        }

        private static void InitializeModel()
        {
            sandboxSettingsModel = new SandboxSettingsModel
            {
                GamesLaunchPath                      = ConstantsUtility.DEFAULT_GAMES_LAUNCH_PATH,
                GameWindowsCount                     = ConstantsUtility.DEFAULT_GAME_WINDOWS_COUNT,
                WindowTitleToFind                    = ConstantsUtility.DEFAULT_WINDOW_TITLE_TO_FIND,
                CredentialsPath                      = ConstantsUtility.DEFAULT_CREDENTIALS_PATH,
                NewGameWindowTitleBeginning          = ConstantsUtility.DEFAULT_NEW_GAME_WINDOW_TITLE_BEGINNING,
                NewGameWindowTitleEnd                = ConstantsUtility.DEFAULT_NEW_GAME_WINDOW_TITLE_END,
                AutoLoginWindowIndexes               = new List<int>(),
                RowCountLadder                       = ConstantsUtility.DEFAULT_ROW_COUNT_LADDER,
                ColumnCountLadder                    = ConstantsUtility.DEFAULT_COLUMN_COUNT_LADDER,
                RowSpacingLadder                     = ConstantsUtility.DEFAULT_ROW_SPACING_LADDER,
                ColumnSpacingLadder                  = ConstantsUtility.DEFAULT_COLUMN_SPACING_LADDER,
                WindowGameWidth                      = ConstantsUtility.DEFAULT_WINDOW_GAME_WIDTH,
                WindowGameHeight                     = ConstantsUtility.DEFAULT_WINDOW_GAME_HEIGHT,
                ExitBattleWindowCountPerAction       = ConstantsUtility.DEFAULT_EXIT_BATTLE_WINDOW_COUNT_PER_ACTION,
                BattleStartDelay                     = ConstantsUtility.DEFAULT_BATTLE_START_DELAY,
                BattleExitDelay                      = ConstantsUtility.DEFAULT_BATTLE_EXIT_DELAY, 
            };

            gameWindowGroupModel = new GameWindowGroupModel
            {
                FirstWindowGroup = new GroupDataModel { StartIndex = 1, EndIndex = 14 },
                SecondWindowGroup = new GroupDataModel { StartIndex = 15, EndIndex =  29},
                ThirdWindowGroup  = new GroupDataModel { StartIndex = 30, EndIndex = 44 },
                FourthWindowGroup = new GroupDataModel { StartIndex = 44, EndIndex = 48 }
            };

            gameWindowManagerModel = new GameWindowManagerModel()
            {
                WindowHandles = new Dictionary<int, string>(),
                WindowHandlesValue = new List<string>()
            };

            battleAccountCountModel = new BattleAccountCountModel();
        }

        private static void InitializeController()
        {
            navigationController = new NavigationController();

            // Main:
            gameLauncherController = new GameLauncherController((MainFormView)navigationController.Forms[FormType.MainForm], sandboxSettingsModel, gameWindowManagerModel);
            gameWindowManagerController = new BattleStarterController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowGroupModel, gameWindowManagerModel, gameLauncherController, sandboxSettingsModel);
            mainFormController = new MainFormController((MainFormView)navigationController.Forms[FormType.MainForm], navigationController);
            taskbarGameController = new TaskbarGameController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowManagerModel);
            gameWindowIlluminationController = new GameWindowIlluminationController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowManagerModel);
            autoAuthController = new AutoAuthController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowManagerModel, sandboxSettingsModel);
            autoWindowLadderController = new AutoWindowLadderController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowManagerModel, sandboxSettingsModel);
            battleExitController = new BattleExitController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowGroupModel, gameWindowManagerModel, gameLauncherController, sandboxSettingsModel);
            windowHandlesController = new WindowHandlesController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowManagerModel, sandboxSettingsModel);
            closeAllGameWindowsController = new CloseAllGameWindowsController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowManagerModel);
            centerAllWindowsController = new CenterAllWindowsController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowManagerModel, sandboxSettingsModel);
            countBattleAccountsController = new CountBattleAccountsController((MainFormView)navigationController.Forms[FormType.MainForm], (DebugCountBattleAccountsView)navigationController.Forms[FormType.DebugCountBattleAccountsForm], battleAccountCountModel);
            resetDisplaysCountBattleAccountsController = new ResetDisplaysCountBattleAccountsController((MainFormView)navigationController.Forms[FormType.MainForm], battleAccountCountModel);
            minimizeAllWindowsController = new MinimizeAllWindowsController((MainFormView)navigationController.Forms[FormType.MainForm], gameWindowManagerModel);
            // Settings:
            settingsController = new SettingsController((SettingsFormView)navigationController.Forms[FormType.SettingsForm], sandboxSettingsModel, navigationController);
            resetSandboxSettingsController = new ResetSandboxSettingsController((SettingsFormView)navigationController.Forms[FormType.SettingsForm], sandboxSettingsModel);

        }
    }

}
