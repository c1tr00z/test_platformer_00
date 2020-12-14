using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class Timer  {

        #region Events

        public event Action TickEvent;

        #endregion

        #region Accessors

        public float timeLeft { get; private set; }

        public bool IsRunning { get; private set; }

        #endregion

        #region Class Implementation

        public void Start(float time) {
            timeLeft = time;
            IsRunning = true;
        }

        public void Tick() {
            if (!IsRunning) {
                return;
            }
            timeLeft = Mathf.Max(0, timeLeft - Time.deltaTime);
            TickEvent?.Invoke();
            if (timeLeft <= 0) {
                IsRunning = false;
            }
        }

        #endregion
    }
}