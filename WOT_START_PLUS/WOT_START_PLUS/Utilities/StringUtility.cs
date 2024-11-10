

namespace WOT_START_PLUS.Utilities
{
    internal static class StringUtility
    {
        public static string FormatGameWindowTitle(string windowTitleGame, string applicationsName, int index)
        {
            return $"{windowTitleGame.Replace("[#]", $"[{applicationsName}{index}] [#]")}";
        }
    }
}
