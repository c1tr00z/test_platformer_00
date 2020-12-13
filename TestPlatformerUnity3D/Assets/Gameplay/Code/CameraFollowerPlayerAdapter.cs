using c1tr00z.AssistLib.Gameplay;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    [RequireComponent(typeof(CameraFollower))]
    public class CameraFollowerPlayerAdapter : MonoBehaviour {

        #region Private Fields

        private CameraFollower _cameraFollower;

        #endregion
        
        #region Accessors

        public CameraFollower cameraFollower => this.GetCachedComponent(ref _cameraFollower);

        #endregion

        #region Unity Events

        private void OnEnable() {
            PlayerRunner.PlayerSpawned += PlayerRunnerOnPlayerSpawned;
        }

        private void OnDisable() {
            PlayerRunner.PlayerSpawned -= PlayerRunnerOnPlayerSpawned;
        }

        #endregion

        #region Class Implementation

        private void PlayerRunnerOnPlayerSpawned(PlayerRunner player) {
            Debug.LogError(player);
            cameraFollower.target = player.transform;
        }

        #endregion
    }
}