

using AutoItX3Lib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Implementation;
using static System.Windows.Forms.AxHost;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class AutoWindowLadderController
    {
        private readonly MainFormView mainFormView;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly SandboxSettingsModel sandboxSettingsModel;
        private readonly AutoItX3 autoIt;
        internal AutoWindowLadderController(MainFormView mainFormView, GameWindowManagerModel gameWindowManagerModel, SandboxSettingsModel sandboxSettingsModel)
        {
            this.mainFormView = mainFormView;
            this.gameWindowManagerModel = gameWindowManagerModel;
            this.sandboxSettingsModel = sandboxSettingsModel;
            this.mainFormView.RunAutoWindowLadder += RunAutoWindowLadder;
            this.autoIt = new AutoItX3();
        }

        //Выстраивание всех окон друг за другом:
        private void RunAutoWindowLadder(object sender, EventArgs args)
        {
            var windowIndexes = HelperUtility.GetSortWindowHandleIndexesList(gameWindowManagerModel.WindowHandles);
            var windowHandles = gameWindowManagerModel.WindowHandles;
            int startX = 0;// TODO: Вынести в настройки начальное положение выстраивания окон.
            int startY = 0;
            int columnCount = sandboxSettingsModel.ColumnCountLadder;
            int currentIndex = 0; // Добавляем переменную для отслеживания порядкового номера окна в списке

            foreach (var windowIndex in windowIndexes)
            {
                //autoIt.WinSetState(windowHandles[windowIndex], "", autoIt.SW_RESTORE);
                autoIt.WinActivate(windowHandles[windowIndex]);

                // Используем currentIndex для расчета позиции вместо windowIndex
                int column = (currentIndex / sandboxSettingsModel.RowCountLadder) % columnCount;
                int row = currentIndex % sandboxSettingsModel.RowCountLadder;
                int x = startX + column * sandboxSettingsModel.ColumnSpacingLadder;
                int y = startY + row * sandboxSettingsModel.RowSpacingLadder;

                // Перемещение окна на новую позицию
                autoIt.WinMove(windowHandles[windowIndex], "", x, y);
                autoIt.WinSetState(windowHandles[windowIndex], "", autoIt.SW_MINIMIZE);

                currentIndex++; // Увеличиваем порядковый номер после обработки каждого окна
            }
        }

        //Выстраивание элементов по их индексу:
        //private void RunAutoWindowLadder(object sender, EventArgs args)
        //{
        //    var windowIndexes = HelperUtility.GetSortWindowHandleIndexesList(gameWindowManagerModel.WindowHandles);

        //    var windowHandles = gameWindowManagerModel.WindowHandles;
        //    int startX = 10; // TODO: Вынести в настройки начальное положение выстраивания окон.
        //    int startY = 10;
        //    int columnCount = sandboxSettingsModel.ColumnCountLadder;

        //    foreach (var windowIndex in windowIndexes)
        //    {
        //        autoIt.WinSetState(windowHandles[windowIndex], "", autoIt.SW_RESTORE);

        //        // Расчет координат для каждого окна с учетом зацикливания
        //        int column = (windowIndex / sandboxSettingsModel.RowCountLadder) % columnCount;
        //        int row = windowIndex % sandboxSettingsModel.RowCountLadder;
        //        int x = startX + column * sandboxSettingsModel.ColumnSpacingLadder;
        //        int y = startY + row * sandboxSettingsModel.RowSpacingLadder;

        //        // Перемещение окна на новую позицию
        //        autoIt.WinMove(windowHandles[windowIndex], "", x, y);
        //        autoIt.Sleep(500); // попробовать добавить задержку без слипа
        //        autoIt.WinSetState(windowHandles[windowIndex], "", autoIt.SW_MINIMIZE);
        //    }

        //    MessageBox.Show("Лестница из аккаунтов выстроена.");
        //}
    }
}
