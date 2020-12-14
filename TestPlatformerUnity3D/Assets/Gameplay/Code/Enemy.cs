using System;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class Enemy : MonoBehaviour, IDamageable {

        #region Private Fields

        private Rigidbody2D _rigidbody;

        private Life _life;

        private bool _isMoving;
        
        private Vector2 _direction;

        private float _startMovingTime;
        
        private float _movingTime;

        private float _lastMovementTime;

        private float _nextMovementDelay;

        #endregion

        #region Serialized Fields

        [SerializeField] private int _damage;

        [SerializeField] private float _speed;

        [SerializeField]
        private float _minMovingTime;
        
        [SerializeField]
        private float _maxMovingTime;
        
        [SerializeField]
        private float _minNextMovementDelay;
        
        [SerializeField]
        private float _maxNextMovementDelay;

        #endregion

        #region Accessors

        public Rigidbody2D rigidbody2D => this.GetCachedComponent(ref _rigidbody);

        #endregion

        #region Unity Events

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.collider.CompareTag(PlayerRunner.PLAYER_RUNNER_TAG)) {
                other.collider.GetComponentInParent<PlayerRunner>().Damage(_damage);
            }
        }

        private void Update() {
            CheckMovement();
        }

        #endregion

        #region IDamageable Implementation

        public Life GetLife() {
            return this.GetCachedComponent(ref _life);
        }

        #endregion

        #region Class Implementation

        private void CheckMovement() {
            if (_isMoving) {
                if (Time.time - _startMovingTime < _movingTime) {
                    rigidbody2D.velocity = _speed * _direction;
                } else {
                    _lastMovementTime = Time.time;
                    _nextMovementDelay = Random.Range(_minNextMovementDelay, _maxNextMovementDelay);
                    rigidbody2D.velocity = Vector2.zero;
                    _isMoving = false;
                }
            } else {
                if (Time.time - _lastMovementTime >= _nextMovementDelay) {
                    _direction = _direction == Vector2.zero ? Vector2.right : _direction * -1;
                    _movingTime = Random.Range(_minMovingTime, _maxMovingTime);
                    _startMovingTime = Time.time;
                    _isMoving = true;
                }
            }
        }

        #endregion
    }
}