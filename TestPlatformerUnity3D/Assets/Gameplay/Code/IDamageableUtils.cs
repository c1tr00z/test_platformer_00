namespace c1tr00z.TestPlatformer.Gameplay {
    public static class IDamageableUtils {
        public static void Damage(this IDamageable damageable, int damageValue) {
            damageable.GetLife().Damage(damageValue);
        }

        public static void Instakill(this IDamageable damageable) {
            damageable.Damage(damageable.GetLife().maxLife);
        }
    }
}