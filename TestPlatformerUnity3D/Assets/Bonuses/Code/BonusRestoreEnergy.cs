using c1tr00z.AssistLib.AppModules;
using c1tr00z.TestPlatformer.Gameplay;

namespace Bonuses.Code {
    public class BonusRestoreEnergy : Bonus<BonusRestoreEnergyDBEntry> {

        #region Bonus Implementation

        public BonusRestoreEnergy(BonusDBEntry dbEntry, BonusesController controller) : base(dbEntry, controller) {
            
        }

        public override void Activate() {
            Modules.Get<GameplayController>().AddEnergy(bonusDBEntry.value);
        }

        #endregion
    }
}