

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WOT_START_PLUS.Utilities
{
    internal static class HelperUtility
    {
        public static List<int> GetSortWindowHandleIndexesList(Dictionary<int, string> windowHandles)
        {
            var windowIndexes = new List<int>();

            foreach (var windowIndex in windowHandles.Keys)
                windowIndexes.Add(windowIndex);

            windowIndexes.Sort();
            return windowIndexes;
        }

        public static (int x, int y) GetScreenCenter(int windowWidth, int windowHeight)
        {
            // Получаем размеры основного экрана
            Rectangle screenSize = Screen.PrimaryScreen.Bounds;

            // Вычисляем центральные координаты
            int centerX = screenSize.Width / 2 - windowWidth / 2;
            int centerY = screenSize.Height / 2 - windowHeight / 2;

            // Возвращаем координаты для размещения окна по центру
            return (x: centerX, y: centerY);
        }

        public static int GetWindowCountInGroup(int startIndex, int endIndex)
        {
            return endIndex - startIndex;
        }

        public static int ConvertSecondsToMilliseconds(float seconds)
        {
            return (int)(seconds * 1000);
        }
    }
}
