using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class Level : MonoBehaviour {

        #region Private Fields

        private Dictionary<LevelPieceBaseDBEntry, List<LevelPiece>> _cachedPieces =
            new Dictionary<LevelPieceBaseDBEntry, List<LevelPiece>>();

        private List<LevelPieceRegularDBEntry> _piecesDBEntries = new List<LevelPieceRegularDBEntry>();

        private LevelGeneratingSettings _levelGeneratingSettings;

        private Transform _cachedItemsContainer;

        #endregion

        #region Serialized Fields

        [SerializeField]
        private LevelPieceStart _startPiece;
        
        #endregion

        #region Accessors

        public LevelPieceStart startPiece {
            get => _startPiece;
            set {
                if (_startPiece.IsAssigned()) {
                    _startPiece.gameObject.Destroy();
                }

                _startPiece = value;
            }
        }

        public LevelGeneratingSettings levelGeneratingSettings => DBEntryUtils.GetCached(ref _levelGeneratingSettings);

        public Transform cachedItemsContainer {
            get {
                if (!_cachedItemsContainer.IsAssigned()) {
                    _cachedItemsContainer = new GameObject("CachedItemsContainer").transform;
                    _cachedItemsContainer.transform.Reset(transform);
                    _cachedItemsContainer.gameObject.SetActive(false);
                }

                return _cachedItemsContainer;
            }
        }
        
        private List<LevelPiece> passedPieces { get; set; } = new List<LevelPiece>();
        
        private LevelPiece currentLevelPiece { get; set; }
        
        private LevelPiece lastGeneratedPiece { get; set; }

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

        public void Init() {
            Cache();
            GenerateStartPieces();
        }

        private void Cache() {
            _piecesDBEntries = DB.GetAll<LevelPieceRegularDBEntry>();
        }

        public void GenerateStartPieces() {
            _startPiece = DB.GetAll<LevelPieceStartDBEntry>().FirstOrDefault()
                .LoadPrefab<LevelPieceStart>().Clone(transform);
            _startPiece.transform.Reset();
            lastGeneratedPiece = _startPiece;
            new [] {levelGeneratingSettings.startPiecesCount}.Iterate(i => {
                var levelPiece = GetLevelPieceByDBEntry(_piecesDBEntries.RandomItem());
                levelPiece.transform.position = lastGeneratedPiece.pieceFinishPoint.position;
                lastGeneratedPiece = levelPiece;
            });
            currentLevelPiece = startPiece;
        }

        private LevelPiece GetLevelPieceByDBEntry(LevelPieceRegularDBEntry dbEntry) {
            LevelPiece levelPiece;
            if (_cachedPieces.ContainsKey(dbEntry)) {
                var list = _cachedPieces[dbEntry];
                levelPiece = list.RandomItem();

                if (levelPiece.IsAssigned()) {
                    list.Remove(levelPiece);
                    return levelPiece;
                }
            }

            levelPiece = dbEntry.LoadPrefab<LevelPiece>().Clone();

            levelPiece.transform.SetParent(transform);
            
            return levelPiece;
        }

        private void ReturnToPool(LevelPiece levelPiece) {
            var dbEntry = levelPiece.dbEntry;
            levelPiece.transform.Reset(cachedItemsContainer);
            if (_cachedPieces.ContainsKey(dbEntry)) {
                _cachedPieces[dbEntry].Add(levelPiece);
                return;
            }
            
            var list = new List<LevelPiece>();
            list.Add(levelPiece);
            _cachedPieces.Add(dbEntry, list);
        }

        private void LevelPieceOnEntered(LevelPiece levelPiece) {
            passedPieces.ForEach(ReturnToPool);
            passedPieces.Clear();
            passedPieces.Add(currentLevelPiece);
            currentLevelPiece = levelPiece;
            var newGeneratedPiece = GetLevelPieceByDBEntry(_piecesDBEntries.RandomItem());
            var pieceTransform = newGeneratedPiece.transform;
            pieceTransform.SetParent(transform);
            pieceTransform.position = lastGeneratedPiece.pieceFinishPoint.position;
            lastGeneratedPiece = newGeneratedPiece;
        }
        
        #endregion
    }
}