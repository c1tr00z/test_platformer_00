using Bonuses.Code;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace Bonuses.UI.Code {
    public class BonusUIView : UIItemView<Bonus> {

        #region Accessors

        public Sprite icon => item.dbEntry.LoadSprite("Icon");

        public string title => item.dbEntry.title;

        #endregion

    }
}