

using System;
using System.Windows.Forms;

namespace WOT_START_PLUS.Views.Configuration.Abstract
{
    internal abstract class BaseForm : Form
    {
        // TODO: не надо открывать новую форму, нужно переключаться между ними!!!
        public BaseForm()
        {
            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
            this.SizeChanged += SettingsFormSizeChanged;
        }

        protected abstract void SettingsFormSizeChanged(object sender, EventArgs args);
    }
}
