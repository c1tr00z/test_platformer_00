using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.DataModels;
using c1tr00z.TestPlatformer.Gameplay;

namespace Gameplay.UI.Code {
    public class GameplayControllerDataModel : DataModelBase {

        #region Accessors

        public GameplayController gameplayController => Modules.Get<GameplayController>();

        #endregion

        #region Class Implementation

        public void Play() {
            gameplayController.Play();
        }

        #endregion
    }
}