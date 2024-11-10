using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WOT_START_PLUS.Enums;
using WOT_START_PLUS.Events;
using WOT_START_PLUS.Models.Configuration;
using AutoItX3Lib;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.Configuration
{
    internal sealed class BattleStarterController
    {
        const int SW_MINIMIZE = 6;
        const int SW_RESTORE = 9;

        private readonly MainFormView mainFormView;

        private readonly GameWindowGroupModel gameWindowGroupModel;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly GameLauncherController gameLauncherController;
        private readonly SandboxSettingsModel sandboxSettingsModel;

        private readonly AutoItX3 autoIt;

        private List<int> currentGroup;
        
        public BattleStarterController(MainFormView mainFormView, GameWindowGroupModel gameWindowGroupModel, GameWindowManagerModel gameWindowManagerModel, GameLauncherController gameLauncherController, SandboxSettingsModel sandboxSettingsModel)
        {
            // добавить кнопку для закрытие всех окон
            // добавить кнопку для сворачивания всех окон
            this.mainFormView = mainFormView;
            this.gameWindowGroupModel = gameWindowGroupModel;
            this.sandboxSettingsModel = sandboxSettingsModel;
            this.gameWindowManagerModel = gameWindowManagerModel;

            this.mainFormView.BattleStartWindowGroup += BattleStartWindowGroup;
            this.mainFormView.RunBattleStartWindowAll += BattleStartWindowAll;

            this.autoIt = new AutoItX3();
        }

        private void BattleStartWindowGroup(object sender, WindowGroupEventArgs args)
        {
            var currentGroup = gameWindowGroupModel.GetGroup(args.GroupType);
            var windowCount = HelperUtility.GetWindowCountInGroup(currentGroup.StartIndex, currentGroup.EndIndex);
            var message = $"Группа <{args.GroupType}> запущена в бой.";

            BattleStartWindow(windowCount, currentGroup.StartIndex, message);

            //// Переписать метод, должен открывать группу, затем прожимать кнопки в бой, после прожатие всех кнопок, сворачивать все. 
            //var currentGroup = gameWindowGroupModel.GetGroup(args.GroupType);

            //for(int currentIndex = currentGroup.StartIndex; currentIndex < currentGroup.EndIndex; currentIndex++)
            //{
            //    if (gameWindowManagerModel.WindowHandles.TryGetValue(currentIndex, out var windowTitle) == false) continue;

            //    autoIt.WinActivate(windowTitle);

            //    int winPosX = autoIt.WinGetPosX(windowTitle);
            //    int winPosY = autoIt.WinGetPosY(windowTitle);
            //    int winWidth = sandboxSettingsModel.WindowGameWidth;

            //    int clickX = winPosX + winWidth / 2;
            //    int clickY = winPosY + ConstantsUtility.START_BUTTON_POSITION_Y;


            //    autoIt.MouseMove(clickX, clickY, 0);
            //    autoIt.MouseClick("LEFT", clickX, clickY);
            //    autoIt.MouseClick("LEFT", clickX, clickY);
            //    autoIt.MouseClick("LEFT", clickX, clickY);
            //    autoIt.Sleep(1000); // TODO: ВЫНЕСТИ ЗАДЕРЖКУ В МОДЕЛЬ
            //    autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
            //}

            //MessageBox.Show("Лестница из аккаунтов выстроена.");
        }

        private void BattleStartWindowAll(object sender, EventArgs args)
        {
            var windowCount = gameWindowManagerModel.WindowHandles.Count;
            var startIndex = 1;
            var message = $"Все аккаунты отправлены в бой.";
            BattleStartWindow(windowCount, startIndex, message);
            //for (int currentIndex = 1; currentIndex <= gameWindowManagerModel.WindowHandles.Count; currentIndex++)
            //{
            //    if (gameWindowManagerModel.WindowHandles.TryGetValue(currentIndex, out var windowTitle))
            //    {
            //        autoIt.WinActivate(windowTitle);

            //        int winPosX = autoIt.WinGetPosX(windowTitle);
            //        int winPosY = autoIt.WinGetPosY(windowTitle);
            //        int winWidth = sandboxSettingsModel.WindowGameWidth;

            //        int clickX = winPosX + winWidth / 2;
            //        int clickY = winPosY + ConstantsUtility.START_BUTTON_POSITION_Y;


            //        autoIt.MouseMove(clickX, clickY, 0);
            //        autoIt.MouseClick("LEFT", clickX, clickY);
            //        autoIt.MouseClick("LEFT", clickX, clickY);
            //        autoIt.MouseClick("LEFT", clickX, clickY);
            //        autoIt.Sleep(1000); // TODO: ВЫНЕСТИ ЗАДЕРЖКУ В МОДЕЛЬ
            //        autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
            //    }
            //    else
            //    {
            //        MessageBox.Show($"Не нашли индекс {currentIndex} при запуске аккаунтов в бой");
            //    }
            //}

        }

        private void BattleStartWindow(int windowCount, int startIndex, string message)
        {

            if (windowCount == 0)
            {
                MessageBox.Show("Нет активных окон.");
                return;
            }

            int totalWindowsProcessed = 0;  // Сколько окон обработано

            // Продолжаем цикл, пока не обработаем все окна
            while (totalWindowsProcessed < windowCount)
            {
                int windowsToProcess = Math.Min(sandboxSettingsModel.ExitBattleWindowCountPerAction, windowCount - totalWindowsProcessed);
                //int startWindow = totalWindowsProcessed;

                // Первый цикл: восстанавливаем окна
                for (int i = 0; i < windowsToProcess; i++)
                {
                    int currentWindowIndex = startIndex + i;
                    if (gameWindowManagerModel.WindowHandles.TryGetValue(currentWindowIndex, out var windowTitle))
                    {
                        autoIt.WinSetState(windowTitle, "", autoIt.SW_RESTORE);
                    }
                }

                for (int i = 0; i < windowsToProcess; i++)
                {
                    int currentWindowIndex = startIndex + i;
                    if (gameWindowManagerModel.WindowHandles.TryGetValue(currentWindowIndex, out var windowTitle))
                    {
                        autoIt.WinActivate(windowTitle);
                        int winPosX = autoIt.WinGetPosX(windowTitle);
                        int winPosY = autoIt.WinGetPosY(windowTitle);
                        int winWidth = sandboxSettingsModel.WindowGameWidth;

                        int clickX = winPosX + winWidth / 2;
                        int clickY = winPosY + ConstantsUtility.START_BUTTON_POSITION_Y;


                        autoIt.MouseMove(clickX, clickY, 0);
                        autoIt.MouseClick("LEFT", clickX, clickY);
                        autoIt.MouseClick("LEFT", clickX, clickY);
                        autoIt.MouseClick("LEFT", clickX, clickY);
                        autoIt.Sleep(HelperUtility.ConvertSecondsToMilliseconds(sandboxSettingsModel.BattleStartDelay)); // TODO: ВЫНЕСТИ ЗАДЕРЖКУ В МОДЕЛЬawdawd
                        autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
                    }
                }

                // Перемещаем начальный индекс вперед на количество обработанных окон
                startIndex += windowsToProcess;
                totalWindowsProcessed += windowsToProcess;
            }

            MessageBox.Show(message);
        }
    }
}
