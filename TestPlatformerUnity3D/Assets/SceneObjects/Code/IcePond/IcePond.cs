using System;
using c1tr00z.TestPlatformer.Gameplay;

namespace c1tr00z.TestPlatformer.SceneObjects {
    public class IcePond : SceneObjectBase {

        #region Accessors

        private IcePondDBEntry icePondDBEntry => dbEntry as IcePondDBEntry;

        #endregion

        #region SceneObjectBase Implementation

        protected override void Interact(PlayerRunner player) {
            player.AddEffect(new IcePondPlayerEffect {
                timeLeft = icePondDBEntry.time
            });
        }

        #endregion
    }
}