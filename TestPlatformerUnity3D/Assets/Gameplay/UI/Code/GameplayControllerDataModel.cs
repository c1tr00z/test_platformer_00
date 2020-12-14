using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.DataModels;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Reference class (data model) for <see cref="GameplayController"/></summary>
     */
    public class GameplayControllerDataModel : DataModelBase {

        #region Accessors

        public GameplayController gameplayController => Modules.Get<GameplayController>();

        public float energy => gameplayController.energy;

        public float maxEnergy => gameplayController.maxEnergy;

        public float energyValue => energy / maxEnergy;

        public bool isCalmMode => gameplayController.level.isCalmMode;

        public Timer calmModeTimer => gameplayController.calmModeTimer;

        #endregion

        #region Unity Events

        private void OnEnable() {
            GameplayController.EnergyChanged += OnDataChanged;
            GameplayController.Updated += OnDataChanged;
        }

        private void OnDisable() {
            GameplayController.EnergyChanged -= OnDataChanged;
            GameplayController.Updated -= OnDataChanged;
        }

        #endregion

        #region Class Implementation

        public void Play() {
            gameplayController.Play();
        }

        #endregion
    }
}