

using System;
using WOT_START_PLUS.Models.Configuration;
using AutoItX3Lib;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using WOT_START_PLUS.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class AutoAuthController
    {
        private readonly MainFormView mainFormView;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly SandboxSettingsModel sandboxSettingsModel;

        private AutoItX3 autoIt;


        // TODO: написать автовыставление режимов и убрать карты
        internal AutoAuthController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel, SandboxSettingsModel sandboxSettingsModel)
        {
            this.mainFormView = mainFormView;
            this.gameWindowManagerModel = gameWindowManagerModel;
            this.sandboxSettingsModel = sandboxSettingsModel;
            this.mainFormView.AutoLoginForAllGameWindows += AutoLoginForAllGameWindows;
            this.mainFormView.AutoLoginForSelectedGameWindows += AutoLoginForSelectedGameWindows;

            autoIt = new AutoItX3();
        }
        // TODO: добавить метод, где нужно указать, в какие номера аккаунтов не произошел вход и скрипт удалит эти строчки из текстового документа

        private void AutoLoginForAllGameWindows(object sender, EventArgs args)
        {
            var windowIndexes = HelperUtility.GetSortWindowHandleIndexesList(gameWindowManagerModel.WindowHandles);
            ReadCredentialsAndAutoLogin(windowIndexes);
            MessageBox.Show("Авторизация в аккаунты выполнена.");
        }

        private void AutoLoginForSelectedGameWindows(object sender, EventArgs args)
        {
            // должен прибавлять gameWindowManagerModel.WindowHandles.Count и запускать аккаунты после уже запущенных + добавить тут удаление аккаунтов, в которые не вошло из .txt
            ReadCredentialsAndAutoLogin(sandboxSettingsModel.AutoLoginWindowIndexes);
            MessageBox.Show("Авторизация в аккаунты выполнена.");
        }

        private void ReadCredentialsAndAutoLogin(List<int> windowIndexes)
        {
            int windowCount = windowIndexes.Count;
            if (windowCount == 0)
            {
                Console.WriteLine("Нет активных окон для автовхода.");
                return;
            }

            var credentials = File.ReadLines(sandboxSettingsModel.CredentialsPath).Take(windowCount).ToList();
            if (credentials.Count < windowCount)
            {
                Console.WriteLine("В файле недостаточно данных для входа во все окна.");
                return;
            }

            var screenCenterData = HelperUtility.GetScreenCenter(sandboxSettingsModel.WindowGameWidth, sandboxSettingsModel.WindowGameHeight);
            foreach (var windowIndex in windowIndexes)
            {
                //string windowTitle = gameWindowManagerModel.WindowHandles.Values.ElementAt(windowIndex);
                string windowTitle = gameWindowManagerModel.WindowHandles[windowIndex];
                AutoLogin(windowTitle, credentials[windowIndex - 1], screenCenterData.x, screenCenterData.y);
            }

            LoadGameWindows(windowIndexes);
        }

        private void AutoLogin(string windowTitle, string credentials, int positionX, int positionY)
        {
            string[] parts = credentials.Split(':');
            if (parts.Length != 2)
            {
                Console.WriteLine("Некорректный формат строки: " + credentials);
                return;
            }

            string login = parts[0];
            string password = parts[1];

            //autoIt.WinSetState(window, "", autoIt.SW_RESTORE);
            autoIt.WinActivate(windowTitle);

            autoIt.WinMove(windowTitle, "", positionX, positionY);

            System.Threading.Thread.Sleep(500);
            autoIt.Send("{TAB 6}");
            System.Threading.Thread.Sleep(500);
            autoIt.Send(login);
            System.Threading.Thread.Sleep(500);
            autoIt.Send("{TAB}");
            System.Threading.Thread.Sleep(500);
            autoIt.Send(password);
            System.Threading.Thread.Sleep(500);
            autoIt.Send("{ENTER}");
            System.Threading.Thread.Sleep(2000);

            autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
        }

        private void LoadGameWindows(List<int> windowIndexes)
        {
            var totalWindows = 0;

            for (int i = 0; i < sandboxSettingsModel.ExitBattleWindowCountPerAction; i++)
            {

                if (gameWindowManagerModel.WindowHandles.TryGetValue(i, out var windowTitle))
                {
                    autoIt.WinSetState(windowTitle, "", autoIt.SW_RESTORE);
                    totalWindows++;
                }
                else continue;
            }

            //autoIt.Sleep(5000);

            for (int i = totalWindows - sandboxSettingsModel.ExitBattleWindowCountPerAction; i < totalWindows; i++)
            {
                if (gameWindowManagerModel.WindowHandles.TryGetValue(i, out var windowTitle))
                {
                    autoIt.WinSetState(windowTitle, "", autoIt.SW_RESTORE);
                    autoIt.Sleep(1000);
                    autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
                    totalWindows++;
                }
                else continue;
            }
        }
    }
}