using System;
using c1tr00z.AssistLib.AppModules;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class InputManager : Module {

        #region Events

        public static event Action<Vector2> InteractionPressed; 

        #endregion

        #region Readonly Fields

        private static readonly string INTERACTION_AXIS = "Interaction";

        #endregion

        #region Private Fields

        private bool _interactionPressedFlag;

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
        }

        #endregion
    }
}