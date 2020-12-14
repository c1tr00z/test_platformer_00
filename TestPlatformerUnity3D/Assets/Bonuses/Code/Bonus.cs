using c1tr00z.AssistLib.ResourcesManagement;

namespace Bonuses.Code {
    /**
     * Executable bonus object. Contian link to bonus DBEntry
     */
    public abstract class Bonus {

        #region Accessors

        public BonusDBEntry dbEntry { get; private set; }

        #endregion

        #region Constructors

        public Bonus(BonusDBEntry dbEntry) {
            this.dbEntry = dbEntry;
        }

        #endregion

        #region Abstract Methods

        public abstract void Activate();

        #endregion

    }

    /**
     * <summary>Generic version of bonus for easier access to data</summary>
     */
    public abstract class Bonus<T> : Bonus where T : BonusDBEntry {

        #region Accessors

        public T bonusDBEntry => dbEntry as T;

        #endregion

        #region Bonus Implementation

        protected Bonus(BonusDBEntry dbEntry) : base(dbEntry) {
        }

        #endregion
    }
}