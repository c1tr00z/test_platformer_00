using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Gameplay class for calling GameplayController.Finish()</summary>
     */
    public class PlayerFail : MonoBehaviour {

        #region Class Implementation
        
        public void TryFail(Collider2D other) {
            if (other.gameObject.CompareTag(PlayerRunner.PLAYER_RUNNER_TAG)) {
                Fail(other.GetComponent<PlayerRunner>());
            }
        }

        public void Fail(PlayerRunner player) {
            if (!player.IsAssigned()) {
                return;
            }
            
            Modules.Get<GameplayController>().Finish();
        }

        #endregion
    }
}