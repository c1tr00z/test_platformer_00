using System;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class PlayerRunner : MonoBehaviour {

        #region Events

        public static event Action<PlayerRunner> PlayerSpawned; 

        #endregion

        #region Readonly Fields

        public static readonly string PLAYER_RUNNER_TAG = "Player";

        #endregion

        #region Private Fields

        private Rigidbody2D _rigidbody2D;

        private GameplaySettings _gameplaySettings;

        private bool _isRunning;

        #endregion

        #region Accessors

        public Rigidbody2D rigidbody2D => this.GetCachedComponent(ref _rigidbody2D);

        private GameplaySettings gameplaySettings => DBEntryUtils.GetCached(ref _gameplaySettings);

        public bool isRunning {//Checking if character can run and rigidbody not kinematic
            get => _isRunning && !rigidbody2D.isKinematic;
            private set => _isRunning = value;
        }

        #endregion

        #region Unity Events

        private void Awake() {
            PlayerSpawned?.Invoke(this);
            isRunning = false;
            rigidbody2D.isKinematic = true;
        }

        private void LateUpdate() {
            CheckSpeed();
        }

        #endregion

        #region Class Implementation

        public void Run() {
            rigidbody2D.isKinematic = false;
            isRunning = true;
        }

        private void CheckSpeed() {
            if (!isRunning) {
                return;
            }

            //Keeping constant horizontal velocity
            var currentVelocity = rigidbody2D.velocity;
            currentVelocity.x = gameplaySettings.playerSpeed;
            rigidbody2D.velocity = currentVelocity;
        }

        public void FullStop() {
            isRunning = false;
            rigidbody2D.isKinematic = true;
        }

        public void StopRunning() {
            isRunning = false;
        }

        #endregion
    }
}