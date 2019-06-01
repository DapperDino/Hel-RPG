using Hel.Targeting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Combat
{
    public class EnemyDamageable : MonoBehaviour, IDamageable, ITargetable
    {
        [Required] [SerializeField] private Transform targetTransform;
        [Required] [SerializeField] private StatsHolder statsHolder;

        public Transform TargetTransform { get { return targetTransform; } }

        public void DealDamage(DamageData damageData)
        {
            statsHolder.DealDamage(damageData);
        }

        public void Heal(int healAmount)
        {
            statsHolder.Heal(healAmount);
        }
    }
}