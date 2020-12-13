using UnityEngine;

namespace c1tr00z.TestPlatformer.Level {
    public class LevelPieceStart : LevelPiece {

        #region Serialized Fields

        [SerializeField] private Transform _startPoint;

        #endregion

        #region Accessors

        public Transform startPoint => _startPoint;

        #endregion

    }
}