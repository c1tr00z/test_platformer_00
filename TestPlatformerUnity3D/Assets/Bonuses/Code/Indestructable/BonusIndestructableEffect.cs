using System;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.Common;
using c1tr00z.TestPlatformer.Gameplay;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Bonuses.Code {
    public class BonusIndestructableEffect : PlayerEffect {

        #region Public Fields

        public BonusIndestructableDBEntry dbEntry;

        #endregion

        #region Accessors

        public Transform effectTransform { get; private set; }
        
        private int playerLayer { get; set; }

        #endregion

        #region PlayerEffect Implementation

        public override void Start(PlayerRunner player) {
            effectTransform = dbEntry.Load<Transform>("Effect").Clone();
            effectTransform.transform.Reset(player.transform);
            player.GetLife().indestructable = true;
            playerLayer = player.gameObject.layer;
            player.gameObject.SetLayer(GameLayers.PROTECTION_LAYER);
        }

        public override void Finish(PlayerRunner player) {
            Object.Destroy(effectTransform.gameObject);
            player.GetLife().indestructable = false;
            player.gameObject.SetLayer(playerLayer);
        }

        #endregion

        #region Class Implementation

        public void Init(BonusIndestructableDBEntry dbEntry) {
            
        }

        #endregion
    }
}