using Hel.Combat;
using Hel.Targeting;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class HealAction : AbilityAction
    {
        [SerializeField] private int healAmount;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            foreach (ITargetable target in abilityCastData.AbilityActionHandler.CurrentTargets)
            {
                if (target == null) { continue; }
                IDamageable damageable = target.transform.GetComponent<IDamageable>();
                if (damageable == null) { continue; }
                damageable.Heal(healAmount);
            }
            yield return null;
        }
    }
}