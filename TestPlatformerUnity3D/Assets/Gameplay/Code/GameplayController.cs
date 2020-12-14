using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Main gameplay controller. Controls base gameplay elements (player energy, level calm mode timer, etc)</summary>
     */
    public class GameplayController : Module {

        #region Events

        public static event Action EnergyChanged;

        public static event Action Updated;

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
        
        /**
         * Required by specs. Countdowns from value from GameplatSettings.calmModeSeconds and when its 0 then
         * level starts to spawn dangerous level pieces. 
         */
        public Timer calmModeTimer { get; private set; }

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
            CheckCalmModeTimer();
        }

        #endregion

        #region Class Implemetation

        private void Init() {
            _startFrame.Show();
            level.SetCalmMode(true);
            level.Init();
            player = DB.Get<PlayerDBEntry>().LoadPrefab<PlayerRunner>().Clone();
            player.transform.position = level.startPiece.startPoint.position;
        }

        public void Play() {
            energy = maxEnergy;
            _playFrame.Show();
            player.Run();
            calmModeTimer = new Timer();
            calmModeTimer.Start(settings.calmModeSeconds);
            Updated?.Invoke();
        }

        public void Finish() {
            _finishFrame.Show(new GameplayResult {
                distance = player.distance
            });
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

        private void CheckCalmModeTimer() {

            if (!level.isCalmMode) {
                return;
            }
            
            if (calmModeTimer == null) {
                return;
            }
            
            calmModeTimer.Tick();

            if (calmModeTimer.IsRunning) {
                return;
            }
            
            level.SetCalmMode(false);
            
            Updated?.Invoke();
        }

        #endregion
    }
}