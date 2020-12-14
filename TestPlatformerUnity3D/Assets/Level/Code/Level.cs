using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Level {
    /**
     * <summary>Main class for level subsystem. Generates level on full random (except when in calm mode).
     * Reuses level pieces, doesnt recreates them.</summary>
     */
    public class Level : MonoBehaviour {

        #region Private Fields

        private Dictionary<LevelPieceBaseDBEntry, List<LevelPiece>> _cachedPieces =
            new Dictionary<LevelPieceBaseDBEntry, List<LevelPiece>>();

        private List<LevelPieceRegularDBEntry> _piecesDBEntries = new List<LevelPieceRegularDBEntry>();

        private LevelGeneratingSettings _levelGeneratingSettings;

        private Transform _cachedItemsContainer;
        
        private List<LevelPiece> _currentPieces = new List<LevelPiece>();

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
        
        public bool isCalmMode { get; private set; }

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

        /**
         * <summary>Pick next level piece. If there is no free pieces of this DBEntry then creates new.</summary>
         */
        public void GenerateStartPieces() {
            _startPiece = DB.GetAll<LevelPieceStartDBEntry>().FirstOrDefault()
                .LoadPrefab<LevelPieceStart>().Clone(transform);
            _startPiece.transform.Reset();
            lastGeneratedPiece = _startPiece;
            new [] {levelGeneratingSettings.startPiecesCount}.Iterate(i => {
                var levelPiece = GetLevelPieceByDBEntry(GetRandomPiece());
                levelPiece.transform.position = lastGeneratedPiece.pieceFinishPoint.position;
                levelPiece.Place();
                _currentPieces.Add(levelPiece);
                lastGeneratedPiece = levelPiece;
            });
            currentLevelPiece = startPiece;
        }

        /**
         * <summary>If there is no free pieces of this DBEntry then creates new.</summary>
         */
        private LevelPiece GetLevelPieceByDBEntry(LevelPieceRegularDBEntry dbEntry) {
            LevelPiece levelPiece;
            if (_cachedPieces.ContainsKey(dbEntry)) {
                var list = _cachedPieces[dbEntry];
                levelPiece = list.FirstOrDefault();

                if (levelPiece.IsAssigned()) {
                    list.Remove(levelPiece);
                    return levelPiece;
                }
            }

            levelPiece = dbEntry.LoadPrefab<LevelPiece>().Clone();

            levelPiece.transform.SetParent(transform);
            
            return levelPiece;
        }

        /**
         * <summary>Returns level piece back to pool</summary>
         */
        private void ReturnToPool(LevelPiece levelPiece) {
            if (levelPiece == null) {
                return;
            }
            var dbEntry = levelPiece.dbEntry;
            _currentPieces.Remove(levelPiece);
            levelPiece.Hide();
            levelPiece.transform.Reset(cachedItemsContainer);
            if (_cachedPieces.ContainsKey(dbEntry)) {
                _cachedPieces[dbEntry].Add(levelPiece);
                return;
            }
            
            var list = new List<LevelPiece>();
            list.Add(levelPiece);
            _cachedPieces.Add(dbEntry, list);
        }

        /**
         * <summary>Calls when player enters new level piece. All prev pieces goes back to pool, and new adds.</summary>
         */
        private void LevelPieceOnEntered(LevelPiece levelPiece) {
            passedPieces.ForEach(ReturnToPool);
            passedPieces.Clear();
            passedPieces.Add(currentLevelPiece);
            currentLevelPiece = levelPiece;
            var newGeneratedPiece = GetLevelPieceByDBEntry(GetRandomPiece());
            var pieceTransform = newGeneratedPiece.transform;
            pieceTransform.SetParent(transform);
            pieceTransform.position = lastGeneratedPiece.pieceFinishPoint.position;
            _currentPieces.Add(newGeneratedPiece);
            lastGeneratedPiece = newGeneratedPiece;
            lastGeneratedPiece.Place();
        }

        /**
         * <summary>Rolls every level object back. Calls when LevelRollbackManager decides it.</summary>
         */
        public void MovePiecesBack(float value) {
            LevelPiece currentPiece = null;
            _currentPieces.ForEach(p => {
                if (currentPiece.IsAssigned()) {
                    p.transform.position = currentPiece.pieceFinishPoint.position;
                } else {
                    p.transform.position = p.transform.position - new Vector3(value, 0, 0);
                }
                currentPiece = p;
            });
        }

        /**
         * <summary>Sets calm mode. When in calm mode - spawns only Regular (normal, plain, without any perils.
         * When it exists calm mode - next level piece is random.</summary>
         */
        public void SetCalmMode(bool calmMode) {
            isCalmMode = calmMode;
        }

        /**
         * <summary>Gets next random level piece. When in calm mode - returns only Regular (normal, plain, without any perils.
         * When it exists calm mode - next level piece is random.</summary>
         */
        private LevelPieceRegularDBEntry GetRandomPiece() {
            if (isCalmMode) {
                return _piecesDBEntries.RandomItem(p => p.type == LevelPieceType.Regular);
            }

            return _piecesDBEntries.RandomItem();
        }
        
        #endregion
    }
}