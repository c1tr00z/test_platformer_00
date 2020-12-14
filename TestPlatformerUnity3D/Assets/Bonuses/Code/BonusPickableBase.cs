using System;

namespace Bonuses.Code {
    public abstract class BonusPickableBase<T> : BonusPickable where T : Bonus {
        
        #region BonusPickable Implementation

        protected override Bonus GetBonusInstance() {
            return (T) Activator.CreateInstance(typeof(T), dbEntry);
        }

        #endregion
    }
}