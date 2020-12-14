using System.Collections.Generic;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Gameplay;
using UnityEngine;

namespace c1tr00z.TestPlatformer.SceneObjects {
    public class Bomb : SceneObjectBase {

        #region Private Fields

        private List<IDamageable> _damageables = new List<IDamageable>();

        private float _startTime;

        #endregion

        #region Serialize Fields

        [SerializeField] private ParticleSystem _particles;

        [SerializeField] private GameObject _rootObject;

        #endregion

        #region Accessors
        
        public BombDBEntry bombDBEntry => dbEntry as BombDBEntry;

        #endregion

        #region Unity Events

        private void Awake() {
            _startTime = Time.time;
        }

        private void Update() {
            CheckTime();
        }

        #endregion

        #region SceneObjectBase Implementation

        protected override void Interact(PlayerRunner player) { }

        #endregion

        #region Class Implementation

        public void OnExplosionRangeEnter(Collider2D collider2D) {
            var damageable = collider2D.GetComponent<IDamageable>();
            if (damageable == null || _damageables.Contains(damageable)) {
                return;
            }
            _damageables.Add(damageable);
        }
        
        public void OnExplosionRangeExit(Collider2D collider2D) {
            var damageable = collider2D.GetComponentInParent<IDamageable>();
            if (damageable == null || !_damageables.Contains(damageable)) {
                return;
            }
            _damageables.Remove(damageable);
        }

        private void CheckTime() {

            if (!_rootObject.activeSelf) {
                return;
            }
            
            if (Time.time - _startTime < bombDBEntry.timeUntilExplosion) {
                return;
            }
            
            Explode();
        }

        private void Explode() {
            _damageables.SelectNotNull().ForEach(d => d.Damage(bombDBEntry.damage));
            _rootObject.SetActive(false);
            _particles.Play();
        }

        #endregion
    }
}