using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Gameplay;
using c1tr00z.TestPlatformer.Level;
using UnityEngine;

namespace c1tr00z.TestPlatformer.SceneObjects {
    public class SceneObjectsController : Module {

        #region Events

        public static event Action SceneObjectActivated;

        public static event Action CooldownsUpdated;

        #endregion

        #region Private Fields

        private GameplayController _gameplayController;

        private Dictionary<SceneObjectDBEntry, float> _coolDowns = new Dictionary<SceneObjectDBEntry, float>();
        
        private List<SceneObjectDBEntry> _sceneObjectsDBEntries = new List<SceneObjectDBEntry>();

        private SceneObjectDBEntry _activeSceneObjects = null;

        #endregion

        #region Accessors

        public SceneObjectDBEntry activeSceneObject {
            get => _activeSceneObjects;
            set {
                if (_activeSceneObjects == value) {
                    return;
                }
                _activeSceneObjects = value;
                SceneObjectActivated?.Invoke();
            }
        }

        public GameplayController gameplayController => ModulesUtils.GetCachedModule(ref _gameplayController);

        public List<SceneObjectDBEntry> sceneObjectsDBEntries {
            get {
                if (_sceneObjectsDBEntries.Count == 0) {
                    _sceneObjectsDBEntries = DB.GetAll<SceneObjectDBEntry>();
                }

                return _sceneObjectsDBEntries;
            }
        }

        #endregion

        #region Unity Events

        private void OnEnable() {
            InputManager.InteractionPressed += InputManagerOnInteractionPressed;
        }

        private void OnDisable() {
            InputManager.InteractionPressed -= InputManagerOnInteractionPressed;
        }

        private void Update() {
            UpdateCooldowns();
        }

        #endregion

        #region Class Implementation
        
        private void InputManagerOnInteractionPressed(Vector2 mousePosition) {
            
            if (!activeSceneObject.IsAssigned()) {
                return;
            }
            
            var clickRay = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(clickRay, out RaycastHit hit, 1000)) {
                
                if (hit.collider.gameObject.CompareTag(LevelPiece.ROAD_TAG)) {
                    Purchase(activeSceneObject, hit.point, hit.collider.GetComponentInParent<LevelPiece>());
                }
            }
        }

        private void Purchase(SceneObjectDBEntry dbEntry, Vector3 position, LevelPiece levelPiece = null) {
            if (!gameplayController.TryDecreaseEnergy(dbEntry.energyPrice) || GetCooldown(dbEntry) > 0) {
                return;
            }

            var sceneObject = dbEntry.LoadPrefab<SceneObjectBase>().Clone();
            position.z = 0;
            sceneObject.transform.position = position;

            if (levelPiece.IsAssigned()) {
                sceneObject.AttachToLevelPiece(levelPiece);
            }
            
            _coolDowns[activeSceneObject] = activeSceneObject.cooldownSeconds;
        }

        private void UpdateCooldowns() {
            var deltaTime = Time.deltaTime;
            sceneObjectsDBEntries.ForEach(dbEntry => {
                if (!_coolDowns.ContainsKey(dbEntry)) {
                    _coolDowns.Add(dbEntry, 0);
                }
                
                if (_coolDowns[dbEntry] > deltaTime) {
                    _coolDowns[dbEntry] -= deltaTime;
                } else {
                    _coolDowns[dbEntry] = 0;
                }
            });
            CooldownsUpdated?.Invoke();
        }

        public float GetCooldown(SceneObjectDBEntry dbEntry) {
            if (_coolDowns.ContainsKey(dbEntry)) {
                return _coolDowns[dbEntry];
            }

            return 0;
        }

        #endregion
    }
}