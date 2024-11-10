using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WOT_START_PLUS.Controllers.MainForm;

namespace WOT_START_PLUS.Views.Implementation
{
    internal sealed class DebugCountBattleAccountsView : Form
    {
        private PictureBox pictureBoxDisplay;
        private readonly MainFormView mainFormView;
        private readonly CountBattleAccountsController countBattleAccountsController;

        internal DebugCountBattleAccountsView()
        {
            InitializeForm();
            InitializeComponents();
        }

        private void InitializeForm()
        {
            Text = "Debug";
            Size = new Size(1000, 200);
            //FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeComponents()
        {
            pictureBoxDisplay = new PictureBox
            {
                Location = new Point(10, 50),
                Size = new Size(1920, 30),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(pictureBoxDisplay);
        }

        public void UpdateImage(Bitmap screenshot)
        {
            pictureBoxDisplay.Image = screenshot;
            pictureBoxDisplay.Refresh();
        }
    }
}
