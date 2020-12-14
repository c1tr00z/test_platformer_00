namespace c1tr00z.TestPlatformer.Gameplay {
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