using System;

namespace Hel.Combat
{
    /// <summary>
    /// Used to pass around data about a particular instance of damage.
    /// </summary>
    [Serializable]
    public struct DamageData
    {
        public int rawDamage;
        public DamageType damageType;
    }
}
