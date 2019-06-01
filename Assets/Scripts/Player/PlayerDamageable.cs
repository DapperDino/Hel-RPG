using Hel.Combat;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Player
{
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        [Required] [SerializeField] private PlayerStatsHolder playerStatsHolder;

        public void DealDamage(DamageData damageData)
        {
            playerStatsHolder.StatsHolder.DealDamage(damageData);
        }

        public void Heal(int healAmount)
        {
            playerStatsHolder.StatsHolder.Heal(healAmount);
        }
    }
}

