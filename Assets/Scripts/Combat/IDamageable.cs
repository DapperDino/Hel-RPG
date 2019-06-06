namespace Hel.Combat
{
    /// <summary>
    /// Defines any entity that can be involved in combat.
    /// </summary>
    public interface IDamageable
    {
        void DealDamage(DamageData damageData);
        void Heal(int healAmount);
    }
}

