using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Gameplay class for forcing player character to fall</summary>
     */
    public class PlayerFall : MonoBehaviour {
        
        #region Class Implementation

        public void TryFall(Collider2D other) {
            if (other.gameObject.CompareTag(PlayerRunner.PLAYER_RUNNER_TAG)) {
                Fall(other.GetComponentInParent<PlayerRunner>());
            }
        }

        public void Fall(PlayerRunner player) {
            if (!player.IsAssigned()) {
                return;
            }
            
            player.StopRunning();
        }

        #endregion
    }
}