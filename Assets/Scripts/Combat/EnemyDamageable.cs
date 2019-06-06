using Hel.Targeting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Combat
{
    /// <summary>
    /// Handles damage interactions with enemies.
    /// </summary>
    public class EnemyDamageable : MonoBehaviour, IDamageable, ITargetable
    {
        [Required] [SerializeField] private Transform targetTransform = null;
        [Required] [SerializeField] private StatsHolder statsHolder = null;

        public Transform TargetTransform { get { return targetTransform; } }

        public void DealDamage(DamageData damageData) => statsHolder.DealDamage(damageData);

        public void Heal(int healAmount) => statsHolder.Heal(healAmount);
    }
}