using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.TestPlatformer.Gameplay {
    /**
     * <summary>Main gameplay settings class.</summary>
     */
    public class GameplaySettings : DBEntry {

        #region Public Fields

        /**
         * <summary>Base player speed</summary>
         */
        public float playerSpeed;

        /**
         * <summary>Max energy</summary>
         */
        public float maxEnergy;

        /**
         * <summary>Energy preplenish speed</summary>
         */
        public float energyReplenishSpeed;

        /**
         * <summary>Seconds until level start to spawn dangerous pieces</summary>
         */
        public float calmModeSeconds;

        #endregion

    }
}