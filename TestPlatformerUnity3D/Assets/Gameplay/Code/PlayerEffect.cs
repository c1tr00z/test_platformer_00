namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * Base class for player modifiers. Allows to add some temporal modifiers. Processing and checking how much time
     * left happening in <see cref="PlayerRunner"/>
     */
    public abstract class PlayerEffect {

        #region Public Fields

        public float timeLeft;

        #endregion

        #region Class Implementation

        public virtual bool IsCanBeFinished(PlayerRunner player) {
            return true;
        }

        #endregion

        #region Abstract Methods

        public abstract void Start(PlayerRunner player);
        
        public abstract void Finish(PlayerRunner player);

        #endregion

    }
}