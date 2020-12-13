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

        #endregion

        #region Accessors

        public Rigidbody2D rigidbody2D => this.GetCachedComponent(ref _rigidbody2D);

        private GameplaySettings gameplaySettings => DBEntryUtils.GetCached(ref _gameplaySettings);

        public bool isRunning {
            get => !rigidbody2D.isKinematic;
            private set => rigidbody2D.isKinematic = false;
        }

        #endregion

        #region Unity Events

        private void Awake() {
            PlayerSpawned?.Invoke(this);
            isRunning = false;
        }

        private void LateUpdate() {
            CheckSpeed();
        }

        #endregion

        #region Class Implementation

        public void Run() {
            isRunning = true;
        }

        private void CheckSpeed() {
            if (!isRunning) {
                return;
            }

            var currentVelocity = rigidbody2D.velocity;
            currentVelocity.x = gameplaySettings.playerSpeed;
            rigidbody2D.velocity = currentVelocity;
        }

        public void Stop() {
            isRunning = false;
        }

        #endregion
    }
}