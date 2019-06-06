using Hel.Combat;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Player
{
    /// <summary>
    /// Handles damage interactions with the player.
    /// </summary>
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        [Required] [SerializeField] private PlayerStatsDataHolder playerStatsDataHolder = null;

        public void DealDamage(DamageData damageData) => playerStatsDataHolder.StatsHolder.DealDamage(damageData);

        public void Heal(int healAmount) => playerStatsDataHolder.StatsHolder.Heal(healAmount);
    }
}

