using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Main gameplay controller</summary>
     */
    public class GameplayController : Module {

        #region Events

        public static event Action EnergyChanged;

        #endregion

        #region Private Fields

        private GameplaySettings _settings;

        private float _energy;

        #endregion

        #region Serialized Fields

        [SerializeField] private Level.Level _level;

        [SerializeField] private UIFrameDBEntry _startFrame;
        
        [SerializeField] private UIFrameDBEntry _playFrame;

        [SerializeField] private UIFrameDBEntry _finishFrame;

        #endregion

        #region Accessors

        public Level.Level level => _level;

        public PlayerRunner player { get; private set; }

        public GameplaySettings settings => DBEntryUtils.GetCached(ref _settings);

        public float energy {
            get => _energy;
            set {
                if (value > _energy && _energy >= maxEnergy) {
                    return;
                }
                _energy = Mathf.Max(Mathf.Min(maxEnergy, value), 0);
                EnergyChanged?.Invoke();
            }
        }

        public float maxEnergy => settings.maxEnergy;

        #endregion

        #region Unity Events

        private void OnEnable() {
            Life.Died += LifeOnDied;
        }

        private void OnDisable() {
            Life.Died -= LifeOnDied;
        }

        private void Start() {
            Init();
        }

        private void Update() {
            TickEnergy();
        }

        #endregion

        #region Class Implemetation

        private void Init() {
            _startFrame.Show();
            level.Init();
            player = DB.Get<PlayerDBEntry>().LoadPrefab<PlayerRunner>().Clone();
            player.transform.position = level.startPiece.startPoint.position;
        }

        public void Play() {
            energy = maxEnergy;
            _playFrame.Show();
            player.Run();
        }

        public void Finish() {
            _finishFrame.Show();
            player.FullStop();
        }

        private void TickEnergy() {
            if (energy >= settings.maxEnergy) {
                return;
            }
            
            energy += settings.energyReplenishSpeed * Time.deltaTime;
        }

        public bool TryDecreaseEnergy(float value) {
            if (energy < value) {
                return false;
            }

            energy -= value;

            return true;
        }

        public void AddEnergy(float additionalValue) {
            energy += additionalValue;
        }

        private void LifeOnDied(Life life) {
            if (life == player.GetLife()) {
                Finish();
            }
        }

        #endregion
    }
}