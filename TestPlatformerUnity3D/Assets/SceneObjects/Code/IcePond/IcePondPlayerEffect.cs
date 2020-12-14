using System;
using c1tr00z.TestPlatformer.Gameplay;
using UnityEngine;

namespace c1tr00z.TestPlatformer.SceneObjects {
    public class IcePondPlayerEffect : PlayerEffect {

        #region PlayerEffect Implementation

        public override void Start(PlayerRunner player) {
            player.rootObject.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }

        public override void Finish(PlayerRunner player) {
            player.rootObject.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        public override bool IsCanBeFinished(PlayerRunner player) {
            return base.IsCanBeFinished(player) && player.IsNothingAboveHead();
        }

        #endregion
    }
}