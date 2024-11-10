using AutoItX3Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WOT_START_PLUS.Controllers.Configuration;
using WOT_START_PLUS.Enums;
using WOT_START_PLUS.Events;
using WOT_START_PLUS.Models.Configuration;
using WOT_START_PLUS.Utilities;
using WOT_START_PLUS.Views.Implementation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class BattleExitController
    {
        private readonly MainFormView mainFormView;
        private readonly GameWindowGroupModel gameWindowGroupModel;
        private readonly GameWindowManagerModel gameWindowManagerModel;
        private readonly GameLauncherController gameLauncherController;
        private readonly SandboxSettingsModel sandboxSettingsModel;

        private readonly int countSleep = 3;

        private readonly AutoItX3 autoIt;

        private List<int> currentGroup;

        internal BattleExitController(MainFormView mainFormView, GameWindowGroupModel gameWindowGroupModel, GameWindowManagerModel gameWindowManagerModel, GameLauncherController gameLauncherController, SandboxSettingsModel sandboxSettingsModel)
        {
            this.mainFormView = mainFormView;
            this.gameWindowGroupModel = gameWindowGroupModel;
            this.gameWindowManagerModel = gameWindowManagerModel;
            this.gameLauncherController = gameLauncherController;
            this.sandboxSettingsModel = sandboxSettingsModel;

            this.autoIt = new AutoItX3();
            this.currentGroup = new List<int>();

            this.mainFormView.BattleExitWindowGroup += BattleExitWindowGroup;
            this.mainFormView.BattleExitAllWindow += BattleExitAllWindow;
        }

        private void BattleExitAllWindow(object sender, EventArgs args)
        {
            var windowCount = gameWindowManagerModel.WindowHandles.Count;
            var startIndex = 1;
            var message = $"Все аккаунты вышли из боя.";
            BattleExitlWindow(windowCount, startIndex, message);
        }

        private void BattleExitWindowGroup(object sender, WindowGroupEventArgs args)
        {
            var currentGroup = gameWindowGroupModel.GetGroup(args.GroupType);
            var windowCount = HelperUtility.GetWindowCountInGroup(currentGroup.StartIndex, currentGroup.EndIndex);
            var message = $"Группа <{args.GroupType}> вышла из боя.";

            BattleExitlWindow(windowCount, currentGroup.StartIndex, message);
        }

        private void BattleExitlWindow(int windowCount, int startIndex, string message)
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
                        autoIt.Sleep(HelperUtility.ConvertSecondsToMilliseconds(sandboxSettingsModel.BattleExitDelay) / countSleep); // new
                        //autoIt.Sleep(100);
                        autoIt.Send("{ESC}");
                        autoIt.Sleep(HelperUtility.ConvertSecondsToMilliseconds(sandboxSettingsModel.BattleExitDelay) / countSleep); // new
                        //autoIt.Sleep(100);
                        autoIt.Send("{ESC}");
                        autoIt.Sleep(HelperUtility.ConvertSecondsToMilliseconds(sandboxSettingsModel.BattleExitDelay) / countSleep); // new 
                        //autoIt.Sleep(HelperUtility.ConvertSecondsToMilliseconds(sandboxSettingsModel.BattleExitDelay));
                        //autoIt.WinSetState(windowTitle, "", autoIt.SW_MINIMIZE);
                    }
                }

                // TODO: ВОТ ЭТО ВОТ РЕАЛИЗОВАЛ, ХЗ КАК БУДЕТ РАБОТАТЬ // new
                for (int i = 0; i < windowsToProcess; i++)
                {
                    int currentWindowIndex = startIndex + i;
                    if (gameWindowManagerModel.WindowHandles.TryGetValue(currentWindowIndex, out var windowTitle))
                    {
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
