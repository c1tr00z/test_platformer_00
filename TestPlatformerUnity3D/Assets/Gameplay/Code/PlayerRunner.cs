using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Common;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Main player class. Interacts with level design elements, enemies, scene objects and bonuses</summary>
     */
    public class PlayerRunner : MonoBehaviour, IDamageable {

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
        
        private List<PlayerEffect> _effects = new List<PlayerEffect>();

        private Life _life;

        #endregion

        #region Serialized Fields

        [SerializeField] private Transform _rootObject;

        [SerializeField] private Transform _headRaycastPoint;

        #endregion

        #region Accessors

        public Rigidbody2D rigidbody2D => this.GetCachedComponent(ref _rigidbody2D);

        private GameplaySettings gameplaySettings => DBEntryUtils.GetCached(ref _gameplaySettings);

        public bool isRunning {//Checking if character can run and rigidbody is dynamic
            get => _isRunning && rigidbody2D.bodyType == RigidbodyType2D.Dynamic;
            private set => _isRunning = value;
        }

        public Transform rootObject => _rootObject;

        public float speed { get; private set; }

        #endregion

        #region Unity Events

        private void Awake() {
            PlayerSpawned?.Invoke(this);
            isRunning = false;
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            speed = gameplaySettings.playerSpeed;
        }

        private void Update() {
            CheckSpeed();
            CheckEffects();
        }

        #endregion

        #region IDamageable Implementation

        public Life GetLife() {
            return this.GetCachedComponent(ref _life);
        }

        #endregion

        #region Class Implementation

        public void Run() {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            isRunning = true;
        }

        private void CheckSpeed() {
            if (!isRunning) {
                return;
            }

            //Keeping constant horizontal velocity
            var currentVelocity = rigidbody2D.velocity;
            currentVelocity.x = speed;
            rigidbody2D.velocity = currentVelocity;
        }

        public void FullStop() {
            isRunning = false;
            rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        public void StopRunning() {
            isRunning = false;
        }

        public void AddEffect(PlayerEffect effect) {
            effect.Start(this);
            _effects.Add(effect);
        }

        public bool IsNothingAboveHead() {
            
            var layerMask = (1 << GameLayers.WALL_LAYER);
            
            var raycastHit = Physics2D.Raycast(_headRaycastPoint.position, Vector2.up, 2, layerMask);

            return !raycastHit.collider.IsAssigned();
        }

        private void CheckEffects() {
            _effects.ForEach(e => e.timeLeft -= Time.deltaTime);
            var toRemove = _effects.Where(e => e.timeLeft <= 0 && e.IsCanBeFinished(this)).ToList();
            toRemove.ForEach(e => e.Finish(this));
            _effects.RemoveRange(toRemove);
        }

        public void SetSpeed(float newSpeed) {
            speed = newSpeed;
        }

        #endregion
    }
}