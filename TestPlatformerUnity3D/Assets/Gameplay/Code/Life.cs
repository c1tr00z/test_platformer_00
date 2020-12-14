using System;
using c1tr00z.AssistLib.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class Life : MonoBehaviour {

        #region Events

        public static event Action<Life> Damaged;
        
        public static event Action<Life> Died;

        #endregion

        #region Serialized Fields

        [SerializeField]
        private UnityEvent _onDamaged;
        
        [SerializeField]
        private UnityEvent _onDied;

        #endregion

        #region Public Fields

        public int maxLife;
        
        public bool indestructable;

        #endregion

        #region Accessors

        public int currentLife => maxLife - damage;
        
        public int damage { get; private set; }

        public bool isAlive => damage < maxLife;

        #endregion

        #region Class Implementation

        public void Damage(int damageValue) {
            if (indestructable || !isAlive) {
                return;
            }

            damage = Mathf.Min(maxLife, damage + damageValue);

            if (isAlive) {
                OnDamaged();
            } else {
                OnDied();
            }
        }

        private void OnDamaged() {
            if (_onDamaged.IsAssigned()) {
                _onDamaged.Invoke();
            }
            Damaged?.Invoke(this);
        }

        private void OnDied() {
            if (_onDied.IsAssigned()) {
                _onDied.Invoke();
            }
            Died?.Invoke(this);
        }

        #endregion
    }
}