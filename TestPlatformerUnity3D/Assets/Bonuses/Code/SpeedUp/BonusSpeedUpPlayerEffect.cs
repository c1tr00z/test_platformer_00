using System;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Gameplay;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Bonuses.Code.SpeedUp {
    public class BonusSpeedUpPlayerEffect : PlayerEffect {

        #region Public Fields

        public BonusSpeedUpDBEntry dbEntry;

        #endregion

        #region Accessors

        private float playerSpeed { get; set; }
        
        private Transform effectTransform { get; set; }

        #endregion

        #region PlayerEffects Implementation

        public override void Start(PlayerRunner player) {
            effectTransform = dbEntry.Load<Transform>("Effect").Clone();
            playerSpeed = player.speed;
            effectTransform.Reset(player.transform);
            player.SetSpeed(dbEntry.speed);
        }

        public override void Finish(PlayerRunner player) {
            player.SetSpeed(playerSpeed);
            Object.Destroy(effectTransform.gameObject);
        }

        #endregion
    }
}