using c1tr00z.AssistLib.ResourcesManagement;

namespace Bonuses.Code {
    public abstract class Bonus {

        #region Accessors

        public BonusDBEntry dbEntry { get; private set; }
        
        public BonusesController controller { get; private set; }

        #endregion

        #region Constructors

        public Bonus(BonusDBEntry dbEntry, BonusesController controller) {
            this.dbEntry = dbEntry;
            this.controller = controller;
        }

        #endregion

        #region Abstract Methods

        public abstract void Activate();

        #endregion

    }

    public abstract class Bonus<T> : Bonus where T : BonusDBEntry {

        #region Accessors

        public T bonusDBEntry => dbEntry as T;

        #endregion

        #region Bonus Implementation

        protected Bonus(BonusDBEntry dbEntry, BonusesController controller) : base(dbEntry, controller) {
        }

        #endregion
    }
}