
using System;
using System.Windows.Forms;
using WOT_START_PLUS.Views.Configuration.Abstract;
using WOT_START_PLUS.Utilities;
using System.Drawing;



namespace WOT_START_PLUS.Views.Configuration
{
    internal partial class SettingsFormView : Form
    {
        //public List<int> AutoLoginWindowIndexes { get; set; }

        //public int RowCount { get; set; }
        //public int ColumnCount { get; set; }
        //public int RowSpacing { get; set; }
        //public int ColumnSpacing { get; set; }

        // создать 6 точек для хранения списка  количеством аккаунтов и с их позициями, это и будет столбец и строка

        private Label gamesLaunchPathLabel;
        private Label gameWindowsCountLabel;

        private Label windowTitleToFindLabel; 
        private Label credentialsPathLabel;
        private Label newGameWindowTitleLabel;

        private Label rowCountLadderLabel;
        private Label columnCountLadderLabel;
        private Label rowSpacingLadderLabel;
        private Label columnSpacingLadderLabel;

        private TextBox gamesLaunchPathTextBox;
        private TextBox gameWindowsCountTextBox;

        private TextBox windowTitleToFindTextBox;
        private TextBox credentialsPathTextBox;
        private TextBox newGameWindowTitleTextBox;

        private TextBox rowCountTextBox;
        private TextBox columnCountTextBox;
        private TextBox rowSpacingTextBox;
        private TextBox columnSpacingTextBox;

        private Button saveButton;
        private Button backButton;
        private Button resetButton;

        public event EventHandler SaveSettings;
        public event EventHandler GoBackForm;
        public event EventHandler ResetModel;
        // RowCountLadder ColumnCountLadder RowSpacingLadder ColumnSpacingLadder
        // rowCountLadderLabel columnCountLadderLabel rowSpacingLadderLabel columnSpacingLadderLabel
        // rowCountTextBox columnCountTextBox rowSpacingTextBox columnSpacingTextBox
        public string GamesLaunchPath
        {
            get => gamesLaunchPathTextBox.Text;
            set => gamesLaunchPathTextBox.Text = value;
        }

        public string WindowTitleToFind
        {
            get => windowTitleToFindTextBox.Text;
            set => windowTitleToFindTextBox.Text = value;
        }

        public string CredentialsPath
        {
            get => credentialsPathTextBox.Text;
            set => credentialsPathTextBox.Text = value;
        }

        public string NewGameWindowTitle
        {
            get => newGameWindowTitleTextBox.Text;
            set => newGameWindowTitleTextBox.Text = value;
        }

        public int GameWindowsCount
        {
            get
            {
                if (int.TryParse(gameWindowsCountTextBox.Text, out int result)) return result;
                return ConstantsUtility.DEFAULT_GAME_WINDOWS_COUNT;
            }
            set => gameWindowsCountTextBox.Text = value.ToString();
        }
        // rowCountTextBox columnCountTextBox rowSpacingTextBox columnSpacingTextBox

        public int RowCountLadder
        {
            get
            {
                if (int.TryParse(rowCountTextBox.Text, out int result)) return result;
                return ConstantsUtility.DEFAULT_ROW_COUNT_LADDER;
            }
            set => rowCountTextBox.Text = value.ToString();
        }

        public int ColumnCountLadder
        {
            get
            {
                if (int.TryParse(columnCountTextBox.Text, out int result)) return result;
                return ConstantsUtility.DEFAULT_COLUMN_COUNT_LADDER;
            }
            set => columnCountTextBox.Text = value.ToString();
        }

        public int RowSpacingLadder
        {
            get
            {
                if (int.TryParse(rowSpacingTextBox.Text, out int result)) return result;
                return ConstantsUtility.DEFAULT_ROW_SPACING_LADDER;
            }
            set => rowSpacingTextBox.Text = value.ToString();
        }

        public int ColumnSpacingLadder
        {
            get
            {
                if (int.TryParse(columnSpacingTextBox.Text, out int result)) return result;
                return ConstantsUtility.DEFAULT_COLUMN_SPACING_LADDER;
            }
            set => columnSpacingTextBox.Text = value.ToString();
        }

        public SettingsFormView() => InitializeComponent();

        public void UpdateLabels(string currentGamesLaunchPath, int currentGameWindowsCount, string currentWindowTitleToFind, string currentCredentialsPath, string currentNewGameWindowTitle, int currentRowCountLadder, int currentColumnCountLadder, int currentRowSpacingLadder, int currentColumnSpacingLadder)
        {
            gamesLaunchPathLabel.Text = $"{ConstantsUtility.GAMES_LAUNCH_PATH_LABEL}{currentGamesLaunchPath}";
            gameWindowsCountLabel.Text = $"{ConstantsUtility.GAME_WINDOWS_COUNT_LABEL}{currentGameWindowsCount}";

            windowTitleToFindLabel.Text = $"{ConstantsUtility.WINDOW_TITLE_TO_FIND_LABEL}{currentWindowTitleToFind}";
            credentialsPathLabel.Text = $"{ConstantsUtility.CREDENTIALS_PATH_LABEL}{currentCredentialsPath}";
            newGameWindowTitleLabel.Text = $"{ConstantsUtility.NEW_GAME_WINDOW_TITLE_LABEL}{currentNewGameWindowTitle}";

            rowCountLadderLabel.Text = $"{ConstantsUtility.ROW_COUNT_LADDER_LABEL}{currentRowCountLadder}";
            columnCountLadderLabel.Text = $"{ConstantsUtility.COLUMN_COUNT_LADDER_LABEL}{currentColumnCountLadder}";
            rowSpacingLadderLabel.Text = $"{ConstantsUtility.ROW_SPACING_LADDER_LABEL}{currentRowSpacingLadder}";
            columnSpacingLadderLabel.Text = $"{ConstantsUtility.COLUMN_SPACING_LADDER_LABEL}{currentColumnSpacingLadder}";
            // RowCountLadder ColumnCountLadder RowSpacingLadder ColumnSpacingLadder
        }

        public void ResetTextBoxes()
        {
            gamesLaunchPathTextBox.Text = string.Empty;
            gameWindowsCountTextBox.Text = string.Empty;
            windowTitleToFindTextBox.Text = string.Empty;
            credentialsPathTextBox.Text = string.Empty;
            newGameWindowTitleTextBox.Text = string.Empty;

            rowCountTextBox.Text = string.Empty;
            columnCountTextBox.Text = string.Empty;
            rowSpacingTextBox.Text = string.Empty;
            columnSpacingTextBox.Text = string.Empty;

            // rowCountTextBox columnCountTextBox rowSpacingTextBox columnSpacingTextBox
        }

        private void InitializeComponent()
        {
            InitializeForm();
            InitializeLabeledInput();
            InitializeButton();
        }

        private void InitializeLabeledInput()
        {

            gamesLaunchPathLabel = new Label();
            gamesLaunchPathLabel.Location = new System.Drawing.Point(20, ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            gamesLaunchPathLabel.AutoSize = true;

            gamesLaunchPathTextBox = new TextBox();
            gamesLaunchPathTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, gamesLaunchPathLabel.Top);
            gamesLaunchPathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gamesLaunchPathTextBox.Width = this.Width - gamesLaunchPathTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;

            // GamesPath:
            gameWindowsCountLabel = new Label();
            gameWindowsCountLabel.Location = new System.Drawing.Point(20, gamesLaunchPathLabel.Bottom + ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            gameWindowsCountLabel.AutoSize = true;

            gameWindowsCountTextBox = new TextBox();
            gameWindowsCountTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, gameWindowsCountLabel.Top);
            gameWindowsCountTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gameWindowsCountTextBox.Width = this.Width - gameWindowsCountTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;
            // NEW
            windowTitleToFindLabel = new Label();
            windowTitleToFindLabel.Location = new System.Drawing.Point(20, gameWindowsCountLabel.Bottom + ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            windowTitleToFindLabel.AutoSize = true;

            windowTitleToFindTextBox = new TextBox();
            windowTitleToFindTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, windowTitleToFindLabel.Top);
            windowTitleToFindTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            windowTitleToFindTextBox.Width = this.Width - windowTitleToFindTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;

            credentialsPathLabel = new Label();
            credentialsPathLabel.Location = new System.Drawing.Point(20, windowTitleToFindLabel.Bottom + ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            credentialsPathLabel.AutoSize = true;

            credentialsPathTextBox = new TextBox();
            credentialsPathTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, credentialsPathLabel.Top);
            credentialsPathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            credentialsPathTextBox.Width = this.Width - credentialsPathTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;

            newGameWindowTitleLabel = new Label();
            newGameWindowTitleLabel.Location = new System.Drawing.Point(20, credentialsPathLabel.Bottom + ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            newGameWindowTitleLabel.AutoSize = true;

            newGameWindowTitleTextBox = new TextBox();
            newGameWindowTitleTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, newGameWindowTitleLabel.Top);
            newGameWindowTitleTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            newGameWindowTitleTextBox.Width = this.Width - newGameWindowTitleTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;

            // new
            rowCountLadderLabel = new Label();
            rowCountLadderLabel.Location = new System.Drawing.Point(20, newGameWindowTitleLabel.Bottom + ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            rowCountLadderLabel.AutoSize = true;

            rowCountTextBox = new TextBox();
            rowCountTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, rowCountLadderLabel.Top);
            rowCountTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rowCountTextBox.Width = this.Width - rowCountTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;

            columnCountLadderLabel = new Label();
            columnCountLadderLabel.Location = new System.Drawing.Point(20, rowCountLadderLabel.Bottom + ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            columnCountLadderLabel.AutoSize = true;

            columnCountTextBox = new TextBox();
            columnCountTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, columnCountLadderLabel.Top);
            columnCountTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            columnCountTextBox.Width = this.Width - columnCountTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;

            rowSpacingLadderLabel = new Label();
            rowSpacingLadderLabel.Location = new System.Drawing.Point(20, columnCountLadderLabel.Bottom + ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            rowSpacingLadderLabel.AutoSize = true;

            rowSpacingTextBox = new TextBox();
            rowSpacingTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, rowSpacingLadderLabel.Top);
            rowSpacingTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rowSpacingTextBox.Width = this.Width - rowSpacingTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;

            columnSpacingLadderLabel = new Label();
            columnSpacingLadderLabel.Location = new System.Drawing.Point(20, rowSpacingLadderLabel.Bottom + ConstantsUtility.DEFAULT_VERTICAL_SPACING);
            columnSpacingLadderLabel.AutoSize = true;

            columnSpacingTextBox = new TextBox();
            columnSpacingTextBox.Location = new System.Drawing.Point(150 + ConstantsUtility.DEFAULT_LEFT_PADDING, columnSpacingLadderLabel.Top);
            columnSpacingTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            columnSpacingTextBox.Width = this.Width - columnSpacingTextBox.Location.X - ConstantsUtility.DEFAULT_RIGHT_PADDING;


            Controls.Add(gamesLaunchPathLabel);
            Controls.Add(gameWindowsCountLabel);
            Controls.Add(windowTitleToFindLabel);
            Controls.Add(credentialsPathLabel);
            Controls.Add(newGameWindowTitleLabel);
            Controls.Add(rowCountLadderLabel);
            Controls.Add(columnCountLadderLabel);
            Controls.Add(rowSpacingLadderLabel);
            Controls.Add(columnSpacingLadderLabel);

            Controls.Add(gamesLaunchPathTextBox);
            Controls.Add(gameWindowsCountTextBox);
            Controls.Add(windowTitleToFindTextBox);
            Controls.Add(credentialsPathTextBox);
            Controls.Add(newGameWindowTitleTextBox);
            Controls.Add(rowCountTextBox);
            Controls.Add(columnCountTextBox);
            Controls.Add(rowSpacingTextBox);
            Controls.Add(columnSpacingTextBox);

            // autoLoginWindowIndexes
        }

        private void InitializeButton()
        {
            // Создаем панель для кнопок
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,  // Прикрепляем панель к нижней части формы
                Height = 150              // Задаем высоту панели
            };

            // Создаем кнопку "Save"
            saveButton = new Button
            {
                Text = "Save",
                Location = new System.Drawing.Point(10, 10),  // Отступы внутри панели
                Size = new System.Drawing.Size(buttonPanel.Width - 20, 40),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };
            saveButton.Click += (object sender, EventArgs args) => SaveSettings?.Invoke(this, EventArgs.Empty);

            // Создаем кнопку "Reset"
            resetButton = new Button
            {
                Text = "Reset",
                Location = new System.Drawing.Point(10, 60),   // Расположение относительно верха панели
                Size = new System.Drawing.Size(buttonPanel.Width - 20, 40),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };
            resetButton.Click += (object sender, EventArgs args) => ResetModel?.Invoke(this, EventArgs.Empty);

            // Создаем кнопку "Back"
            backButton = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(10, 110),
                Size = new System.Drawing.Size(buttonPanel.Width - 20, 40),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };
            backButton.Click += (object sender, EventArgs args) => GoBackForm?.Invoke(this, EventArgs.Empty);

            // Добавляем кнопки в панель и панель в форму
            buttonPanel.Controls.Add(saveButton);
            buttonPanel.Controls.Add(resetButton);
            buttonPanel.Controls.Add(backButton);
            Controls.Add(buttonPanel);
        }

        private void InitializeForm()
        {
            Text = "Settings";
            Size = new Size(714, 585);
            //FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
