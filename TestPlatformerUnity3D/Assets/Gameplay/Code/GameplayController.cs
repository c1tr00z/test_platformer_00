using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using UnityEngine;

namespace c1tr00z.TestPlatformer.Gameplay {
    public class GameplayController : Module {

        #region Serialized Fields

        [SerializeField] private Level _level;

        [SerializeField] private UIFrameDBEntry _startFrame;
        
        [SerializeField] private UIFrameDBEntry _playFrame;

        [SerializeField] private UIFrameDBEntry _finishFrame;

        #endregion


        #region Accessors

        public Level level => _level;

        public PlayerRunner player { get; private set; }

        #endregion

        #region Unity Events

        private void Start() {
            Init();
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
            _playFrame.Show();
            player.Run();
        }

        public void Finish() {
            _finishFrame.Show();
            player.Stop();
        }

        #endregion
    }
}