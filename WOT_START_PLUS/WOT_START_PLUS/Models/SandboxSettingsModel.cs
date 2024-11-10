

using System.Collections.Generic;

namespace WOT_START_PLUS.Models.Configuration
{
    internal sealed class SandboxSettingsModel
    {
        public string GamesLaunchPath { get; set; }
        public int GameWindowsCount { get; set; }
        public List<int> AutoLoginWindowIndexes { get; set; }
        public string WindowTitleToFind { get; set; }
        public string CredentialsPath { get; set; }
        public string NewGameWindowTitleBeginning { get; set; }
        public string NewGameWindowTitleEnd { get; set; }

        // Ladder:
        public int RowCountLadder { get; set; }
        public int ColumnCountLadder { get; set; }
        public int RowSpacingLadder { get; set; }
        public int ColumnSpacingLadder { get; set; }

        // GameWindowSize:
        public int WindowGameWidth { get; set; }
        public int WindowGameHeight { get; set; }

        public int ExitBattleWindowCountPerAction { get; set; }

        // GroupElemelt


        // Delay:
        public float BattleStartDelay { get; set; }
        public float BattleExitDelay { get; set; }
        public float LoginOnServerRestartDelay { get; set; }

    }
}
