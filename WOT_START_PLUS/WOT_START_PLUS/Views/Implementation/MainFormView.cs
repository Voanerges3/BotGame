using System;
using System.Drawing;
using System.Windows.Forms;
using WOT_START_PLUS.Enums;
using WOT_START_PLUS.Events;
using WOT_START_PLUS.Utilities;

namespace WOT_START_PLUS.Views.Implementation
{
    internal sealed class MainFormView : Form
    {
        public event EventHandler RunOpenSandbox;
        public event EventHandler OpenSettings;
        public event EventHandler RunSortGameWindowsOnTaskbar;
        public event EventHandler IlluminationRequested;
        public event EventHandler AutoLoginForAllGameWindows;
        public event EventHandler AutoLoginForSelectedGameWindows;
        public event EventHandler RunAutoWindowLadder;
        public event EventHandler BattleExitAllWindow;
        public event EventHandler RunUpdateWindowHandles;
        public event EventHandler RunCloseAllGameWindows;
        public event EventHandler RunBattleStartWindowAll;
        public event EventHandler RunCenterAllWindows;
        public event EventHandler RunMinimizeAllWindows;
        public event EventHandler<WindowGroupEventArgs> BattleStartWindowGroup;
        public event EventHandler<WindowGroupEventArgs> BattleExitWindowGroup;

        public event EventHandler CountBattleAccounts;
        public event EventHandler ResetDisplays;
        public event EventHandler ResetWindowBacklight;

        public event EventHandler OpenDebug;

        private Label battleAccountCountDisplayOneLabel;
        private Label battleAccountCountDisplayTwoLabel;
        private Label battleAccountCountDisplayThreeLabel;

        private int withGroupBox = 200; 
        private int withStandertButtod = 180; 
        

        internal MainFormView()
        {
            InitializeForm();
            InitializeComponents();
        }

        public void UpdateView(int battleAccountCountDisplayOne, int battleAccountCountDisplayTwo, int battleAccountCountDisplayThree)
        {
            battleAccountCountDisplayOneLabel.Text = battleAccountCountDisplayOne.ToString();
            battleAccountCountDisplayTwoLabel.Text = battleAccountCountDisplayTwo.ToString();
            battleAccountCountDisplayThreeLabel.Text = battleAccountCountDisplayThree.ToString();
        }

        private void InitializeForm()
        {
            Text = "Main";
            Size = new Size(445, 605);
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeComponents()
        {
            // Добавить дисплей для отображения прогресс бара
            // Добавить дисплей для отображения количество аккаунтов в бою
            GroupBox groupBorRunning = CreateGroupBox("Запуск", new Point(10, 5), withGroupBox, 110);
            AddButtonToGroup(groupBorRunning, "Запустить", 20, withStandertButtod, (object sender, EventArgs e) => RunOpenSandbox?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBorRunning, "Авторизоваться во всех", 50, withStandertButtod, (object sender, EventArgs e) => AutoLoginForAllGameWindows?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBorRunning, "Авторизоваться в выбранных", 80, withStandertButtod, (object sender, EventArgs e) => AutoLoginForSelectedGameWindows?.Invoke(this, EventArgs.Empty));

            GroupBox groupBoxSystem = CreateGroupBox("Система", new Point(10, groupBorRunning.Bottom + 5), withGroupBox, 140);
            AddButtonToGroup(groupBoxSystem, "Настройки", 20, withStandertButtod, (object sender, EventArgs args) => OpenSettings?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBoxSystem, "Обновить обработчик окон", 50, withStandertButtod, (object sender, EventArgs e) => RunUpdateWindowHandles?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBoxSystem, "Остановить выполнение", 80, withStandertButtod, Null);
            AddButtonToGroup(groupBoxSystem, "Закрыть все аккаунты", 110, withStandertButtod, (object sender, EventArgs e) => RunCloseAllGameWindows?.Invoke(this, EventArgs.Empty)); 

            GroupBox groupBoxWindows = CreateGroupBox("Управление окнами", new Point(groupBorRunning.Right + 10, 5), withGroupBox, 350);
            AddButtonToGroup(groupBoxWindows, "Выстроить лестницу", 20, withStandertButtod, (object sender, EventArgs e) => RunAutoWindowLadder?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBoxWindows, "Выставить уровень танков", 50, withStandertButtod, Null);
            AddButtonToGroup(groupBoxWindows, "Выставление режимов", 80, withStandertButtod, Null);
            AddButtonToGroup(groupBoxWindows, "Отключить запись реплеев", 110, withStandertButtod, Null);
            AddButtonToGroup(groupBoxWindows, "Выставление карт", 140, withStandertButtod, Null);
            AddButtonToGroup(groupBoxWindows, "Завершение разминки", 170, withStandertButtod, Null);
            AddButtonToGroup(groupBoxWindows, "Подсчитать количество аккаунтов в бою", 200, withStandertButtod, Null);
            AddButtonToGroup(groupBoxWindows, "Вход после перезагрузки", 230, withStandertButtod, Null);
            AddButtonToGroup(groupBoxWindows, "Аккаунты по центру", 260, withStandertButtod, (object sender, EventArgs e) => RunCenterAllWindows?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBoxWindows, "Проверка очереди", 290, withStandertButtod, Null);
            AddButtonToGroup(groupBoxWindows, "Свернуть все окна", 320, withStandertButtod, (object sender, EventArgs e) => RunMinimizeAllWindows?.Invoke(this, EventArgs.Empty));

            GroupBox groupBoxOptimization = CreateGroupBox("Оптимизация", new Point(10, groupBoxSystem.Bottom + 5), withGroupBox, 80);
            AddButtonToGroup(groupBoxOptimization, "[-] Снять ограничение на аккаунты", 20, withStandertButtod, Null);
            AddButtonToGroup(groupBoxOptimization, "Заменить текстуры", 50, withStandertButtod, Null);

            GroupBox groupBoxStartAndEndBattle = CreateGroupBox("Войти/выйти в бой", new Point(10, groupBoxOptimization.Bottom + 5), withGroupBox, 80);
            AddButtonToGroup(groupBoxStartAndEndBattle, "Запустить все аккаунты", 20, withStandertButtod, (object sender, EventArgs e) => RunBattleStartWindowAll?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBoxStartAndEndBattle, "Выйти всеми аккаунтами", 50, withStandertButtod, (object sender, EventArgs e) => BattleExitAllWindow?.Invoke(this, EventArgs.Empty));

            GroupBox groupBoxStartGroupBattle = CreateGroupBox("Групповой запуск", new Point(10, groupBoxStartAndEndBattle.Bottom + 5), withGroupBox, 50, new Point(5, -2));
            AddButtonToGroup(groupBoxStartGroupBattle, "1", new Point(10, 20), new Size(23,23), (object sender, EventArgs args) => BattleStartWindowGroup?.Invoke(this, new WindowGroupEventArgs(WindowGroupType.FirstWindowGroup)));
            AddButtonToGroup(groupBoxStartGroupBattle, "2", new Point(35, 20), new Size(23, 23),(object sender, EventArgs args) => BattleStartWindowGroup?.Invoke(this, new WindowGroupEventArgs(WindowGroupType.SecondWindowGroup)));
            AddButtonToGroup(groupBoxStartGroupBattle, "3", new Point(60, 20), new Size(23, 23), (object sender, EventArgs args) => BattleStartWindowGroup?.Invoke(this, new WindowGroupEventArgs(WindowGroupType.ThirdWindowGroup)));
            AddButtonToGroup(groupBoxStartGroupBattle, "4", new Point(85, 20), new Size(23, 23), (object sender, EventArgs args) => BattleStartWindowGroup?.Invoke(this, new WindowGroupEventArgs(WindowGroupType.FourthWindowGroup)));
            AddButtonToGroup(groupBoxStartGroupBattle, "★★★★", new Point(120, 20), new Size(69, 23), Null);

            GroupBox groupBoxEndGroupBattle = CreateGroupBox("Групповой выход", new Point(10, groupBoxStartGroupBattle.Bottom), withGroupBox, 50, new Point(5, -2));
            AddButtonToGroup(groupBoxEndGroupBattle, "1", new Point(10, 20), new Size(23, 23), (object sender, EventArgs args) => BattleExitWindowGroup?.Invoke(this, new WindowGroupEventArgs(WindowGroupType.FirstWindowGroup)));
            AddButtonToGroup(groupBoxEndGroupBattle, "2", new Point(35, 20), new Size(23, 23), (object sender, EventArgs args) => BattleExitWindowGroup?.Invoke(this, new WindowGroupEventArgs(WindowGroupType.SecondWindowGroup)));
            AddButtonToGroup(groupBoxEndGroupBattle, "3", new Point(60, 20), new Size(23, 23), (object sender, EventArgs args) => BattleExitWindowGroup?.Invoke(this, new WindowGroupEventArgs(WindowGroupType.ThirdWindowGroup)));
            AddButtonToGroup(groupBoxEndGroupBattle, "4", new Point(85, 20), new Size(23, 23), (object sender, EventArgs args) => IlluminationRequested?.Invoke(this, new WindowGroupEventArgs(WindowGroupType.FourthWindowGroup)));
            AddButtonToGroup(groupBoxEndGroupBattle, "★★★★", new Point(120, 20), new Size(69, 23), Null);

            GroupBox groupBoxbattleAccountCount = CreateGroupBox("Подсчет аккаунтов", new Point(groupBorRunning.Right + 10, groupBoxWindows.Bottom + 5), withGroupBox, 195);
            AddButtonToGroup(groupBoxbattleAccountCount, "Подсчитать", 20, withStandertButtod, (object sender, EventArgs e) => CountBattleAccounts?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBoxbattleAccountCount, "Обнулить дисплеи", 50, withStandertButtod, (object sender, EventArgs e) => ResetDisplays?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBoxbattleAccountCount, "Сбросить подсветку", 80, withStandertButtod, (object sender, EventArgs e) => ResetWindowBacklight?.Invoke(this, EventArgs.Empty));
            AddButtonToGroup(groupBoxbattleAccountCount, "Отладка", 110, withStandertButtod, (object sender, EventArgs e) => OpenDebug?.Invoke(this, EventArgs.Empty));
            var panelbattleAccountCount = AddPanelToGroup(groupBoxbattleAccountCount, new Size(groupBoxbattleAccountCount.Width - 22, 46), new Point(11, 140));

            CreateLabelForPanel(ref battleAccountCountDisplayOneLabel, panelbattleAccountCount,1);
            CreateLabelForPanel(ref battleAccountCountDisplayTwoLabel, panelbattleAccountCount,2);
            CreateLabelForPanel(ref battleAccountCountDisplayThreeLabel, panelbattleAccountCount,3);

            Controls.Add(groupBorRunning);
            Controls.Add(groupBoxWindows);
            Controls.Add(groupBoxStartAndEndBattle);
            Controls.Add(groupBoxSystem);
            Controls.Add(groupBoxOptimization);
            Controls.Add(groupBoxStartGroupBattle);
            Controls.Add(groupBoxEndGroupBattle);
            Controls.Add(groupBoxbattleAccountCount);
        }

        private GroupBox CreateGroupBox(string title, Point location, int width, int height)
        {
            GroupBox groupBox = new GroupBox
            {
                Text = "",
                Location = location,
                Size = new Size(width, height),
                // Отключение автоматической отрисовки текста
            };

            groupBox.Paint += (sender, args) =>
            {
                // Отрисовка жирного шрифта для заголовка
                TextRenderer.DrawText(args.Graphics, title, new Font("Segoe UI", 9, FontStyle.Bold),
                                      new Point(10, -2), SystemColors.ControlText);
            };

            return groupBox;
        }

        private GroupBox CreateGroupBox(string title, Point location, int width, int height, Point textPosition)
        {
            GroupBox groupBox = new GroupBox
            {
                Text = "",
                Location = location,
                Size = new Size(width, height),
                // Отключение автоматической отрисовки текста
            };

            groupBox.Paint += (sender, args) =>
            {
                // Отрисовка жирного шрифта для заголовка
                TextRenderer.DrawText(args.Graphics, title, new Font("Segoe UI", 9, FontStyle.Bold),
                                      textPosition, SystemColors.ControlText);
            };

            return groupBox;
        }

        private void AddButtonToGroup(GroupBox groupBox, string buttonText, int yOffset, int width , EventHandler eventHandler)
        {
            Button button = new Button
            {
                Text = buttonText,
                Size = new Size(width, 23),
                Location = new Point((groupBox.Size.Width - width) / 2, yOffset) // Центрирование кнопки
            };
            button.Click += (object sender, EventArgs args) => eventHandler?.Invoke(this, EventArgs.Empty);
            groupBox.Controls.Add(button);
        }

        private void AddButtonToGroup(GroupBox groupBox, string buttonText, Point location, Size size, EventHandler eventHandler)
        {
            Button button = new Button
            {
                Text = buttonText,
                Size = size,
                Location = location
            };
            button.Click += (object sender, EventArgs args) => eventHandler?.Invoke(this, EventArgs.Empty);
            groupBox.Controls.Add(button);
        }

        private Panel AddPanelToGroup(GroupBox groupBox, Size size, Point location)
        {
            var panel = new Panel()
            {
                Size = size,
                Location = location,
                BackColor = Color.Gray,
                BorderStyle = BorderStyle.None
            };
            panel.Paint += PanelCustomPaint;

            groupBox.Controls.Add(panel);
            return panel;
        }

        private void CreateLabelForPanel(ref Label label,Panel panel, int part)
        {
            var offset = ConstantsUtility.OFFSET_LABEL_FOR_PANEL;

            label = new Label()
            {
                AutoSize = true,
                Text = "00",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 22, FontStyle.Regular)
            };
            panel.Controls.Add(label);

            // Обновление позиции и размера label в соответствии с частью панели
            int thirdWidth = panel.Width / 3;
            int labelWidth = thirdWidth;
            int labelHeight = panel.Height;

            // Расчёт позиции для label
            int left = (part - 1) * thirdWidth + (thirdWidth - label.Width) / 2 + offset;
            int top = (panel.Height - label.Height) / 2 + offset;

            label.Location = new Point(left, top);
            label.Size = new Size(labelWidth, labelHeight);
        }


        private void PanelCustomPaint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            int thirdWidth = panel.Width / 3;
            float penWidth = 1.5f; // Толщина пера
            using (var pen = new Pen(Color.Black, penWidth))
            {
                // Корректируем координаты для учета толщины пера
                float offset = penWidth / 2;

                // Рисуем толстую обводку вокруг панели
                e.Graphics.DrawRectangle(pen, offset, offset, panel.Width - 1 - penWidth, panel.Height - 1 - penWidth);

                // Рисуем вертикальные линии, чтобы разделить панель на три части
                e.Graphics.DrawLine(pen, new Point(thirdWidth, 0), new Point(thirdWidth, panel.Height));
                e.Graphics.DrawLine(pen, new Point(2 * thirdWidth, 0), new Point(2 * thirdWidth, panel.Height));
            }
        }

        private void Null(object sender, EventArgs e)
        {
            MessageBox.Show("Функция не написана!");
        }
    }
}






















