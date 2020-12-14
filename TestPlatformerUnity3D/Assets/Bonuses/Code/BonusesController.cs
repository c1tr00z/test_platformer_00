using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bonuses.Code {
    /**
     * <summary>Controls everything about bonuses.</summary>
     */
    public class BonusesController : Module {

        #region Events

        public static event Action<Bonus> BonusPicked;
        
        public static event Action<Bonus> BonusActivated;

        #endregion

        #region Private Fields

        private BonusesSettings _settings;
        
        /**
         * Cached bonus pickable objects
         */
        private List<BonusPickable> _cachedBonuses = new List<BonusPickable>();

        /**
         * Container for storing cached bonus pickable objects
         */
        private Transform _cachedBonusesContainer;
        
        private List<BonusDBEntry> _bonusesDBEntries = new List<BonusDBEntry>();

        #endregion

        #region Accessors

        public List<Bonus> pickedBonuses { get; } = new List<Bonus>();

        public BonusesSettings settings => DBEntryUtils.GetCached(ref _settings);

        public List<BonusDBEntry> bonusesDBEntries {
            get {
                if (_bonusesDBEntries.Count == 0) {
                    _bonusesDBEntries = DB.GetAll<BonusDBEntry>();
                }

                return _bonusesDBEntries;
            }
        }

        public Transform cachedBonusesContainer {
            get {
                if (!_cachedBonusesContainer.IsAssigned()) {
                    _cachedBonusesContainer = new GameObject("CachedContainer").transform;
                    _cachedBonusesContainer.SetParent(transform);
                    _cachedBonusesContainer.gameObject.SetActive(false);
                }

                return _cachedBonusesContainer;
            }
        }

        #endregion

        #region Unity Events

        private void OnEnable() {
            LevelPiece.Placed += LevelPieceOnPlaced;
        }

        private void OnDisable() {
            LevelPiece.Placed -= LevelPieceOnPlaced;
        }

        #endregion

        #region Class Implementation

        public void AddBonus(Bonus bonus) {
            if (pickedBonuses.Count == settings.maxBonusesCount) {
                pickedBonuses.Remove(pickedBonuses.FirstOrDefault());
            }
            pickedBonuses.Add(bonus);
            BonusPicked?.Invoke(bonus);
        }

        public void Activate(Bonus bonus) {
            pickedBonuses.Remove(bonus);
            bonus.Activate();
            BonusActivated?.Invoke(bonus);
        }

        private void LevelPieceOnPlaced(LevelPiece levelPiece) {
            if (!levelPiece.bonusPoint.IsAssigned()) {
                return;
            }

            var chance = Random.Range(0f, 1f);

            if (chance > settings.bonusChance) {
                return;
            }

            var bonusPickable = GetPickableFromPool(bonusesDBEntries.RandomItem());
            
            bonusPickable.AttackToLevelPiece(levelPiece);
        }

        private BonusPickable GetPickableFromPool(BonusDBEntry dbEntry) {
            var bonus = _cachedBonuses.RandomItem(b => b.dbEntry == dbEntry);
            if (bonus.IsAssigned()) {
                _cachedBonuses.Remove(bonus);
            } else {
                bonus = dbEntry.LoadPrefab<BonusPickable>().Clone();
            }

            return bonus;
        }

        public void ReturnToPool(BonusPickable bonusPickable) {
            _cachedBonuses.Add(bonusPickable);
            bonusPickable.transform.Reset(cachedBonusesContainer);
        }

        #endregion
    }
}