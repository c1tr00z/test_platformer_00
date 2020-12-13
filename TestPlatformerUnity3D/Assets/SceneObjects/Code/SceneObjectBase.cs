using System;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Gameplay;
using c1tr00z.TestPlatformer.Level;
using UnityEngine;

namespace c1tr00z.TestPlatformer.SceneObjects {
    public abstract class SceneObjectBase : MonoBehaviour {

        #region Private Fields

        private DBEntryResource _dbEntryResource;

        #endregion

        #region Accessors

        private DBEntryResource dbEntryResource => this.GetCachedComponent(ref _dbEntryResource);
        
        public SceneObjectDBEntry dbEntry => dbEntryResource.parent as SceneObjectDBEntry;
        
        public LevelPiece levelPiece { get; private set; }

        #endregion

        #region Unity Events

        private void OnEnable() {
            LevelPiece.Hidden += LevelPieceOnHidden;
        }

        private void OnDisable() {
            LevelPiece.Hidden -= LevelPieceOnHidden;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag(PlayerRunner.PLAYER_RUNNER_TAG)) {
                Interact(other.GetComponent<PlayerRunner>());
            }
        }

        #endregion

        #region Abstract Methods

        protected abstract void Interact(PlayerRunner player);

        #endregion

        #region Class Implementation

        public void AttachToLevelPiece(LevelPiece levelPiece) {
            this.levelPiece = levelPiece;
            transform.SetParent(levelPiece.transform);
        }

        private void LevelPieceOnHidden(LevelPiece levelPiece) {
            if (this.levelPiece == levelPiece) {
                //TODO: Return it to pool instead of destruction
                Destroy(gameObject);
            }
        }

        #endregion
    }
}