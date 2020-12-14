using c1tr00z.TestPlatformer.Gameplay;
using UnityEngine;

namespace c1tr00z.TestPlatformer.SceneObjects {
    public class JumpPod : SceneObjectBase {

        #region Accessors

        private JumpPodDBEntry jumpPodDBEntry => dbEntry as JumpPodDBEntry;

        #endregion

        #region SceneObjectBase Implementation

        protected override void Interact(PlayerRunner player) {
            player.rigidbody2D.AddForce(Vector2.up * jumpPodDBEntry.jumpForce);
        }

        #endregion
    }
}