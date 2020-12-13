using System;
using c1tr00z.AssistLib.AppModules;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Input Adapter manager. Needed to avoid using checking axis in gameplay code</summary>
     */
    public class InputManager : Module {

        #region Events

        public static event Action<Vector2> InteractionPressed;
        public static event Action<Vector2> DeathClickPressed;

        #endregion

        #region Readonly Fields

        private static readonly string INTERACTION_AXIS = "Interaction";
        private static readonly string DEATH_CLICK_AXIS = "DeathClick";

        #endregion

        #region Private Fields

        private bool _interactionPressedFlag;
        private bool _deathClickPressedFlag;

        #endregion

        #region Unity Events

        private void Update() {
            CheckInput();
        }

        #endregion

        #region Class Implementation

        private void CheckInput() {
            if (Input.GetAxis(INTERACTION_AXIS) > 0 && !_interactionPressedFlag) {
                InteractionPressed?.Invoke(Input.mousePosition);
                _interactionPressedFlag = true;
            } else if (Input.GetAxis(INTERACTION_AXIS) == 0 && _interactionPressedFlag) {
                _interactionPressedFlag = false;
            }
            
            if (Input.GetAxis(DEATH_CLICK_AXIS) > 0 && !_deathClickPressedFlag) {
                DeathClickPressed?.Invoke(Input.mousePosition);
                _deathClickPressedFlag = true;
            } else if (Input.GetAxis(DEATH_CLICK_AXIS) == 0 && _deathClickPressedFlag) {
                _deathClickPressedFlag = false;
            }
        }

        #endregion
    }
}