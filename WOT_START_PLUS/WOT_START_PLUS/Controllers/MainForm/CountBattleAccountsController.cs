using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WOT_START_PLUS.Models;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class CountBattleAccountsController
    {
        private readonly MainFormView wainFormView;
        private readonly DebugCountBattleAccountsView debugCountBattleAccountsView;
        private readonly BattleAccountCountModel battleAccountCountModel;

        public event EventHandler UpdateScreenshot;

        private readonly int mergeDistance = 31; // Расстояние объединения пикселей в одну область
        private readonly int maxWidth = 31; // Максимальная ширина области
        private readonly int maxHeight = 31; // Максимальная высота области
        private readonly int minWidth = 20; // Минимальная ширина области
        private readonly int minHeight = 20; // Минимальная высота области

        internal CountBattleAccountsController(MainFormView wainFormView, DebugCountBattleAccountsView debugCountBattleAccountsView, BattleAccountCountModel battleAccountCountModel)
        {
            this.wainFormView = wainFormView;
            this.debugCountBattleAccountsView = debugCountBattleAccountsView;
            this.battleAccountCountModel = battleAccountCountModel;
            this.wainFormView.CountBattleAccounts += CountBattleAccounts;
        }

        private void CountBattleAccounts(object sender, EventArgs args)
        {
            battleAccountCountModel.OperationCount++;

            Bitmap screenshot = CaptureScreen();

            List<Rectangle> colorAreas = FindColorAreas(screenshot);
            DrawColorAreas(screenshot, colorAreas);
            UpdateModel(colorAreas.Count);
            UpdateView(screenshot);
        }

        // перенести operationCount в model и если ресет нажали, то обнулить до 0 
        private void UpdateModel(int colorAreasCound)
        {
            if(battleAccountCountModel.OperationCount == ConstantsUtility.OPERATIONS_UNTIL_RESET)
            {
                battleAccountCountModel.BattleAccountCountDisplayThree = default;
                battleAccountCountModel.BattleAccountCountDisplayTwo   = default;
                battleAccountCountModel.BattleAccountCountDisplayOne   = default;

                battleAccountCountModel.OperationCount                 = default;
            }

            battleAccountCountModel.BattleAccountCountDisplayThree = battleAccountCountModel.BattleAccountCountDisplayTwo;
            battleAccountCountModel.BattleAccountCountDisplayTwo = battleAccountCountModel.BattleAccountCountDisplayOne;

            var battleAccountCount = colorAreasCound - battleAccountCountModel.BattleAccountCountDisplayTwo - battleAccountCountModel.BattleAccountCountDisplayThree;
            battleAccountCountModel.BattleAccountCountDisplayOne = battleAccountCount;
        }

        private void UpdateView(Bitmap screenshot)
        {
            wainFormView.UpdateView(battleAccountCountModel.BattleAccountCountDisplayOne,
                                    battleAccountCountModel.BattleAccountCountDisplayTwo,
                                  battleAccountCountModel.BattleAccountCountDisplayThree);


            var isDugFormOpen = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form is DebugCountBattleAccountsView && form.Text == "Debug") // Проверяем тип формы и заголовок
                {
                    isDugFormOpen = true;
                    break;
                }
            }

            if (!isDugFormOpen) return;

            debugCountBattleAccountsView.UpdateImage(screenshot);
        }


        private Bitmap CaptureScreen()
        {
            int taskbarHeight = 30;
            Rectangle screenRect = new Rectangle(0, Screen.PrimaryScreen.Bounds.Height - taskbarHeight,
                                                 Screen.PrimaryScreen.Bounds.Width, taskbarHeight);
            Bitmap bmp = new Bitmap(screenRect.Width, screenRect.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(screenRect.Location, Point.Empty, screenRect.Size);
            }
            return bmp;
        }

        private List<Rectangle> FindColorAreas(Bitmap image)
        {
            List<Rectangle> colorAreas = new List<Rectangle>();
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData bmpData = image.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * image.Height;
            byte[] rgbValues = new byte[bytes];
            bool[] visited = new bool[image.Width * image.Height];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int index = y * image.Width + x;
                    if (!visited[index] && IsColorInRange(rgbValues[index * 3 + 2], rgbValues[index * 3 + 1], rgbValues[index * 3]))
                    {
                        List<Point> regionPoints = new List<Point>();
                        FloodFill(rgbValues, visited, x, y, image.Width, image.Height, regionPoints);
                        if (regionPoints.Count > 0)
                        {
                            Rectangle area = UnifyRegions(regionPoints);
                            if (!area.IsEmpty)
                            {
                                colorAreas.Add(area);
                            }
                        }
                    }
                }
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            image.UnlockBits(bmpData);
            return colorAreas;
        }

        private bool IsColorInRange(byte red, byte green, byte blue)
        {
            return red > 150 && green < red / 2 && blue < 60;
        }

        private void FloodFill(byte[] rgbValues, bool[] visited, int x, int y, int width, int height, List<Point> regionPoints)
        {
            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((x, y));

            while (stack.Count > 0)
            {
                var (cx, cy) = stack.Pop();
                if (visited[cy * width + cx]) continue;
                visited[cy * width + cx] = true;
                regionPoints.Add(new Point(cx, cy));

                var neighbours = GetNeighbours(cx, cy, width, height);
                foreach (var (nx, ny) in neighbours)
                {
                    if (!visited[ny * width + nx] && IsColorInRange(rgbValues[ny * width * 3 + nx * 3 + 2], rgbValues[ny * width * 3 + nx * 3 + 1], rgbValues[ny * width * 3 + nx * 3]))
                    {
                        stack.Push((nx, ny));
                    }
                }
            }
        }

        private List<(int, int)> GetNeighbours(int x, int y, int width, int height)
        {
            List<(int, int)> neighbours = new List<(int, int)>();
            // Проверка только непосредственных соседей
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i], ny = y + dy[i];
                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    neighbours.Add((nx, ny));
                }
            }

            return neighbours;
        }

        private Rectangle UnifyRegions(List<Point> points)
        {
            if (points.Count == 0) return Rectangle.Empty;

            int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
            foreach (var point in points)
            {
                minX = Math.Min(minX, point.X);
                minY = Math.Min(minY, point.Y);
                maxX = Math.Max(maxX, point.X);
                maxY = Math.Max(maxY, point.Y);
            }

            int width = maxX - minX + 1;
            int height = maxY - minY + 1;

            if (width < minWidth || height < minHeight)
            {
                return Rectangle.Empty;
            }

            width = Math.Min(width, maxWidth);
            height = Math.Min(height, maxHeight);

            return new Rectangle(minX, minY, width, height);
        }


        private void DrawColorAreas(Bitmap image, List<Rectangle> colorAreas)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                foreach (var area in colorAreas)
                {
                    using (Brush greenBrush = new SolidBrush(Color.FromArgb(128, 0, 255, 0)))
                    {
                        g.FillRectangle(greenBrush, area);
                    }
                    g.DrawRectangle(Pens.Green, area);
                }
            }
        }
    }
}
