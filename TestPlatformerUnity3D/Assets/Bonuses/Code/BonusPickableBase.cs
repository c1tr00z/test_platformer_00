using System;

namespace Bonuses.Code {
    public abstract class BonusPickableBase<T> : BonusPickable where T : Bonus {
        
        #region BonusPickable Implementation

        protected override Bonus GetBonusInstance() {
            var args = new[] {
                dbEntry
            };
            return (T) Activator.CreateInstance(typeof(T), args);
        }

        #endregion
    }
}