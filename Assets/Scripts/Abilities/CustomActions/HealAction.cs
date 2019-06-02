using Hel.Combat;
using Hel.Targeting;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to heal the currently collected targets.
    /// </summary>
    [Serializable]
    public class HealAction : AbilityAction
    {
        [SerializeField] private int healAmount = 10;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Loop through each target that we have collected.
            foreach (ITargetable target in abilityCastData.AbilityActionHandler.CurrentTargets)
            {
                //Make sure that the target still exists.
                if (target == null) { continue; }

                //Get the damageable component from the target.
                IDamageable damageable = target.transform.GetComponent<IDamageable>();

                //Make sure that the target is damageable.
                if (damageable == null) { continue; }

                //Heal the target damageable by the desired heal amount.
                damageable.Heal(healAmount);
            }

            yield return null;
        }
    }
}