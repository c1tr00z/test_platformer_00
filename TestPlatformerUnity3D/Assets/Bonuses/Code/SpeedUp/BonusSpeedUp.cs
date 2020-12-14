using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.TestPlatformer.Gameplay;

namespace Bonuses.Code.SpeedUp {
    public class BonusSpeedUp : Bonus<BonusSpeedUpDBEntry> {

        #region Bonus Implementation

        public BonusSpeedUp(BonusDBEntry dbEntry) : base(dbEntry) {
        }

        public override void Activate() {
            var effect = new BonusSpeedUpPlayerEffect {
                dbEntry = bonusDBEntry,
                timeLeft = bonusDBEntry.time
            };
            Modules.Get<GameplayController>().player.AddEffect(effect);
        }

        #endregion
    }
}