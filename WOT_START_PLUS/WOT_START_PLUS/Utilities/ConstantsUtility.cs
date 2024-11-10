using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOT_START_PLUS.Enums;

namespace WOT_START_PLUS.Utilities
{
    internal static class ConstantsUtility
    {
        // Form:
        public const FormType STARTUP_FORM = FormType.MainForm;

        // Default Value:
        //public const string DEFAULT_GAMES_LAUNCH_PATH = "C:\\Games\\World_of_Tanks_EU\\WorldOfTanks.exe"; // EU
        public const string DEFAULT_GAMES_LAUNCH_PATH = "C:\\Games\\Tanki\\WorldOfTanks.exe"; // RU
        //public const string DEFAULT_WINDOW_TITLE_TO_FIND = "WoT Client"; // EU
        public const string DEFAULT_WINDOW_TITLE_TO_FIND = "Мир танков"; // RU
        //public const string DEFAULT_CREDENTIALS_PATH = "C:\\Users\\Александр\\Desktop\\Текст\\[EU] Аккаунты\\[EU_1] World of Tanks.txt"; // EU
        public const string DEFAULT_CREDENTIALS_PATH = "C:\\Users\\Александр\\Desktop\\Текст\\[RU] Аккаунты\\[RU_1] World of Tanks.txt"; // RU
        public const string DEFAULT_NEW_GAME_WINDOW_TITLE_BEGINNING = "WOT_";
        public const string DEFAULT_NEW_GAME_WINDOW_TITLE_END = "_EU";
        //public const int DEFAULT_GAME_WINDOWS_COUNT = 48; // EU
        public const int DEFAULT_GAME_WINDOWS_COUNT = 30; // RU
        public const int DEFAULT_EXIT_BATTLE_WINDOW_COUNT_PER_ACTION = 15;

        // Per:
        public const int MILLISECONDS_PER_SECOND = 1000;

        // Offset:
        public const int SMALL_OFFSET = 10;
        public const int MEDIUM_OFFSET = 50;
        public const int LARGE_OFFSET = 100;

        // Label: 
        public const string GAMES_LAUNCH_PATH_LABEL = "CurrentLaunchPath: ";
        public const string DELAY_LABEL = "CurrentDelay: ";
        public const string GAME_WINDOWS_COUNT_LABEL = "CurrentGameWindowsCount: ";
        public const string WINDOW_TITLE_TO_FIND_LABEL = "CurrentWindowTitleToFind: ";
        public const string CREDENTIALS_PATH_LABEL = "CurrentCredentialsPath: ";
        public const string NEW_GAME_WINDOW_TITLE_LABEL = "CurrentNewGameWindowTitle: ";

        public const string ROW_COUNT_LADDER_LABEL = "RowCountLadder: ";
        public const string COLUMN_COUNT_LADDER_LABEL = "ColumnCountLadder: ";
        public const string ROW_SPACING_LADDER_LABEL = "RowSpacingLadder: ";
        public const string COLUMN_SPACING_LADDER_LABEL = "ColumnSpacingLadder: ";

        // PADDING:
        public const int DEFAULT_RIGHT_PADDING = 45;
        public const int DEFAULT_LEFT_PADDING = 35;
        public const int DEFAULT_VERTICAL_SPACING = 10;

        // Ladder:
        // bool поле, выставлять на основании 2 экранов или на основании 1 экрана

        public const int DEFAULT_ROW_COUNT_LADDER = 11;
        public const int DEFAULT_COLUMN_COUNT_LADDER = 4;
        public const int DEFAULT_ROW_SPACING_LADDER = 80;
        public const int DEFAULT_COLUMN_SPACING_LADDER = 900;

        // SizeWindowGame
        public const int DEFAULT_WINDOW_GAME_WIDTH = 1024;
        public const int DEFAULT_WINDOW_GAME_HEIGHT = 768;

        // Position Controller
        public const int START_BUTTON_POSITION_Y = 50;

        //Delay:
        public const float DEFAULT_BATTLE_START_DELAY = 0.5f;
        public const float DEFAULT_BATTLE_EXIT_DELAY = 1.5f;
        public const float DEFAULT_LOGIN_ON_SERVER_RESTART_DELAY = 1f;
        //BattleStartDelay

        // CountBattleAccounts
        public const int OPERATIONS_UNTIL_RESET = 3;

        // Panel
        public const int OFFSET_LABEL_FOR_PANEL = 2;
    }
}
