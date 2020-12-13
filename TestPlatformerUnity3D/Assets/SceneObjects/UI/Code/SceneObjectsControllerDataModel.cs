using System;
using System.Collections.Generic;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.DataModels;
using c1tr00z.TestPlatformer.SceneObjects;

namespace SceneObjects.UI.Code {
    public class SceneObjectsControllerDataModel : DataModelBase {

        #region Private Fields

        private SceneObjectsController _sceneObjectsController;

        #endregion

        #region Accessors

        public SceneObjectsController sceneObjectsController => ModulesUtils.GetCachedModule(ref _sceneObjectsController);

        public List<SceneObjectDBEntry> sceneObjectsDBEntries => sceneObjectsController.sceneObjectsDBEntries;

        public SceneObjectDBEntry activeSceneObject => sceneObjectsController.activeSceneObject;

        #endregion

        #region Unity Events

        private void OnEnable() {
            SceneObjectsController.SceneObjectActivated += SceneObjectsControllerOnSceneObjectActivated; 
        }

        private void OnDisable() {
            SceneObjectsController.SceneObjectActivated -= SceneObjectsControllerOnSceneObjectActivated;
        }

        #endregion

        #region Class Implementation

        public void SelectSceneObjectFromObject(object obj) {
            if (obj is SceneObjectDBEntry sceneObjectDBEntry) {
                SelectSceneObject(sceneObjectDBEntry);
            }
        }

        private void SelectSceneObject(SceneObjectDBEntry dbEntry) {
            if (sceneObjectsController.activeSceneObject == dbEntry) {
                return;
            }
            sceneObjectsController.activeSceneObject = dbEntry;
        }

        private void SceneObjectsControllerOnSceneObjectActivated() {
            OnDataChanged();
        }

        #endregion

    }
}