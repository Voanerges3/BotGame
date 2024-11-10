

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WOT_START_PLUS.Enums;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Configuration;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.Navigation
{
    internal sealed class NavigationController
    {
        // TODO: не надо открывать новую форму, нужно переключаться между ними!!!
        private Form currentForm;
        public Dictionary<FormType, Form> Forms { get; private set; }

        internal NavigationController() => Initialize();

        public void ShowForm(FormType formType)
        {
            Form formToShow;

            if (Forms.TryGetValue(formType, out formToShow))
            {
                //if (currentForm != null)
                //    currentForm.Hide();

                currentForm = formToShow;
                currentForm.Show();
            }
            else
                throw new ArgumentException("Form type not recognized.");
        }

        private void Initialize()
        {
            Forms = new Dictionary<FormType, Form>();

            Forms[FormType.MainForm] = new MainFormView();
            Forms[FormType.SettingsForm] = new SettingsFormView();
            Forms[FormType.DebugCountBattleAccountsForm] = new DebugCountBattleAccountsView();


            currentForm = Forms[ConstantsUtility.STARTUP_FORM];
        }
    }
}
