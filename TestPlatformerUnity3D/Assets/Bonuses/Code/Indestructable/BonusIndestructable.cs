using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.TestPlatformer.Gameplay;

namespace Bonuses.Code {
    public class BonusIndestructable : Bonus<BonusIndestructableDBEntry> {

        #region Bonus Implementation

        public BonusIndestructable(BonusDBEntry dbEntry, BonusesController controller) : base(dbEntry, controller) {
        }

        public override void Activate() {
            var effect = new BonusIndestructableEffect {
                timeLeft = bonusDBEntry.time,
                dbEntry = bonusDBEntry
            };
            Modules.Get<GameplayController>().player.AddEffect(effect);
        }

        #endregion
    }
}