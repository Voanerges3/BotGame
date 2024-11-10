

using System;
using WOT_START_PLUS.Models;
using WOT_START_PLUS.Views.Implementation;

namespace WOT_START_PLUS.Controllers.MainForm
{
    internal sealed class ResetDisplaysCountBattleAccountsController
    {
        private readonly MainFormView mainFormView;
        private readonly BattleAccountCountModel battleAccountCountModel;

        internal ResetDisplaysCountBattleAccountsController(MainFormView mainFormView, BattleAccountCountModel battleAccountCountModel)
        {
            this.mainFormView = mainFormView;
            this.battleAccountCountModel = battleAccountCountModel;

            this.mainFormView.ResetDisplays += ResetDisplays;
        }

        private void ResetDisplays(object sender, EventArgs args)
        {
            battleAccountCountModel.BattleAccountCountDisplayThree = default;
            battleAccountCountModel.BattleAccountCountDisplayTwo = default;
            battleAccountCountModel.BattleAccountCountDisplayOne = default;

            battleAccountCountModel.OperationCount = default;

            mainFormView.UpdateView(battleAccountCountModel.BattleAccountCountDisplayOne,
                                    battleAccountCountModel.BattleAccountCountDisplayTwo,
                                   battleAccountCountModel.BattleAccountCountDisplayThree);

        }
    }
}
