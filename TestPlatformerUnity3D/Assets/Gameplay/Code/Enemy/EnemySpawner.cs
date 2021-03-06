using System;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Enemy spawner class. Object supposed to be in LevelPiece.</summary>
     */
    public class EnemySpawner : MonoBehaviour {

        #region Private Fields

        private Enemy _enemy;

        #endregion

        #region Unity Events

        private void OnEnable() {
            CheckSpawn();
        }

        #endregion

        #region Class Implementation

        /**
         * <summary>Every OnEnable checks if enemy was spawned. If not - spawn it.</summary>
         */
        private void CheckSpawn() {
            if (_enemy.IsAssigned()) {
                return;
            }

            _enemy = DB.GetAll<EnemyDBEntry>().RandomItem().LoadPrefab<Enemy>().Clone();
            _enemy.transform.position = transform.position;
            _enemy.transform.Reset(transform.parent);
        }

        #endregion

    }
}