using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.AssistLib.Utils;
using c1tr00z.TestPlatformer.SceneObjects;
using UnityEngine;

namespace SceneObjects.UI.Code {
    public class SceneObjectDBEntryUIView : UIItemView<SceneObjectDBEntry> {

        #region Private Fields

        private SceneObjectsController _sceneObjectsController;

        private UIListItem _listItem;

        #endregion

        #region Serialized Fields

        [SerializeField] private Color _normalStateColor = Color.white;
        [SerializeField] private Color _selectedStateColor = Color.white;

        #endregion

        #region Accessors

        public SceneObjectsController sceneObjectsController => ModulesUtils.GetCachedModule(ref _sceneObjectsController);

        private UIListItem listItem => this.GetCachedComponent(ref _listItem);

        public Color color => listItem.isSelected ? _selectedStateColor : _normalStateColor;

        public string title => item.title;

        public float coolDownValue =>
            1f - (item.cooldownSeconds - sceneObjectsController.GetCooldown(item)) / item.cooldownSeconds;

        public Sprite icon => item.LoadSprite("Icon");

        #endregion

        #region Unity Events

        private void OnEnable() {
            SceneObjectsController.CooldownsUpdated += SceneObjectsControllerOnCooldownsUpdated;
        }

        private void OnDisable() {
            SceneObjectsController.CooldownsUpdated -= SceneObjectsControllerOnCooldownsUpdated;
        }

        #endregion

        #region Class Implementation

        private void SceneObjectsControllerOnCooldownsUpdated() {
            OnDataChanged();
        }

        #endregion

    }
}