using c1tr00z.AssistLib.AppModules;
using c1tr00z.TestPlatformer.Level;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class LevelRollbackManager : Module {

        #region Private Fields

        private GameplayController _gameplayController;

        #endregion

        #region Serialized Fields

        [SerializeField] private float _maxX = 1000f;

        #endregion

        #region Accessors

        public GameplayController gameplayController => ModulesUtils.GetCachedModule(ref _gameplayController);

        public PlayerRunner player => gameplayController.player;

        #endregion

        #region Unity Events

        private void OnEnable() {
            LevelPiece.Entered += LevelPieceOnEntered;
        }

        private void OnDisable() {
            LevelPiece.Entered -= LevelPieceOnEntered;
        }

        #endregion

        #region Class Implementation

        private void LevelPieceOnEntered(LevelPiece levelPiece) {
            
            var moveBackValue = player.transform.position.x;
            
            if (moveBackValue < _maxX) {
                return;
            }
            
            gameplayController.level.MovePiecesBack(moveBackValue);
            player.transform.position -= new Vector3(moveBackValue, 0, 0);
        }

        #endregion
    }
}