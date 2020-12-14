using c1tr00z.AssistLib.AppModules;
using c1tr00z.TestPlatformer.Gameplay;

namespace Bonuses.Code {
    public class BonusRestoreEnergy : Bonus<BonusRestoreEnergyDBEntry> {

        #region Bonus Implementation

        public BonusRestoreEnergy(BonusDBEntry dbEntry) : base(dbEntry) {
            
        }

        public override void Activate() {
            Modules.Get<GameplayController>().AddEnergy(bonusDBEntry.value);
        }

        #endregion
    }
}