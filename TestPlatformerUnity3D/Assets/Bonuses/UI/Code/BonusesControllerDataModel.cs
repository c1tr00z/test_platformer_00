using System;
using System.Collections.Generic;
using Bonuses.Code;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.DataModels;

namespace Bonuses.UI.Code {
    public class BonusesControllerDataModel : DataModelBase {

        #region Private Fields

        private BonusesController _bonusesController;

        #endregion

        #region Accesors

        public BonusesController bonusesController => ModulesUtils.GetCachedModule(ref _bonusesController);

        public List<Bonus> pickedBonuses => bonusesController.pickedBonuses;

        #endregion

        #region Unity Events

        private void OnEnable() {
            BonusesController.BonusPicked += BonusesControllerOnBonusPicked;
            BonusesController.BonusActivated += BonusesControllerOnBonusActivated;
        }

        private void OnDisable() {
            BonusesController.BonusPicked -= BonusesControllerOnBonusPicked;
            BonusesController.BonusActivated -= BonusesControllerOnBonusActivated;
        }

        #endregion

        #region Class Implementation

        private void ActivateBonus(Bonus bonus) {
            bonusesController.Activate(bonus);
        }

        public void TryActivateBonusFromObject(object obj) {
            if (obj is Bonus bonus) {
                ActivateBonus(bonus);
            }
        }

        private void BonusesControllerOnBonusPicked(Bonus bonus) {
            OnDataChanged();
        }

        private void BonusesControllerOnBonusActivated(Bonus bonus) {
            OnDataChanged();
        }

        #endregion
    }
}