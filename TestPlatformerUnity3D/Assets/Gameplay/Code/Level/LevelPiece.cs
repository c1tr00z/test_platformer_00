using System;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class LevelPiece : MonoBehaviour {

        #region Events

        public static event Action<LevelPiece> Entered; 

        #endregion

        #region Private Fields

        private DBEntryResource _dbEntryResource;

        #endregion

        #region Serialized Fields

        [SerializeField] private Transform _pieceFinishPoint;

        #endregion

        #region Accessors

        public DBEntryResource dbEntryResource => this.GetCachedComponent(ref _dbEntryResource);

        public Transform pieceFinishPoint => _pieceFinishPoint;

        public LevelPieceBaseDBEntry dbEntry => dbEntryResource.parent as LevelPieceBaseDBEntry;

        #endregion

        #region Class Implementation

        public void OnEnterTrigger(Collider2D other) {
            if (other.gameObject.CompareTag(PlayerRunner.PLAYER_RUNNER_TAG)) {
                Enter();
            }
        }

        private void Enter() {
            Entered?.Invoke(this);
        }

        #endregion
    }
}