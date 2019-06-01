namespace Hel.Combat
{
    public interface IDamageable
    {
        void DealDamage(DamageData damageData);
        void Heal(int healAmount);
    }
}

