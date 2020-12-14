using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Gameplay;
using c1tr00z.TestPlatformer.Level;
using UnityEngine;

namespace Bonuses.Code {
    public abstract class BonusPickable : MonoBehaviour {
        
        #region Private Fields

        private BonusesController _bonusesController;

        private DBEntryResource _dbEntryResource;

        #endregion

        #region Accessors

        public BonusesController bonusesController => ModulesUtils.GetCachedModule(ref _bonusesController);

        public DBEntryResource dbEntryResource => this.GetCachedComponent(ref _dbEntryResource);
        
        public BonusDBEntry dbEntry => dbEntryResource.parent as BonusDBEntry;
        
        public LevelPiece levelPiece { get; private set; }

        #endregion

        #region Unity Events

        private void OnEnable() {
            LevelPiece.Hidden += LevelPieceOnHidden;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag(PlayerRunner.PLAYER_RUNNER_TAG)) {
                bonusesController.AddBonus(GetBonusInstance());
                bonusesController.ReturnToPool(this);
            }
        }

        #endregion

        #region Class Implementation

        public void AttackToLevelPiece(LevelPiece levelPiece) {
            this.levelPiece = levelPiece;
            transform.SetParent(levelPiece.transform);
            transform.position = levelPiece.bonusPoint.position;
        }

        private void LevelPieceOnHidden(LevelPiece levelPiece) {
            if (this.levelPiece != levelPiece) {
                return;
            }
            
            bonusesController.ReturnToPool(this);
        }

        public void Reset() {
            levelPiece = null;
        }

        #endregion

        #region Abstract Methods

        protected abstract Bonus GetBonusInstance();

        #endregion

    }
}