using System.Text.RegularExpressions;
using c1tr00z.AssistLib.GameUI;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class TimerUIView : UIItemView<Timer> {

        #region Accessors

        public float timeLeft => item != null ? item.timeLeft : 0;

        public int secondsLeftInt => Mathf.CeilToInt(timeLeft);

        #endregion

        #region UIItemView Implementation

        public override bool isDataModelEnabled => true;

        protected override void OnShow(params object[] args) {
            base.OnShow(args);
            if (item == null) {
                return;
            }
            item.TickEvent += OnDataChanged;
        }

        #endregion
    }
}