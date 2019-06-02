using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to put item on cooldown.
    /// </summary>
    [Serializable]
    public class PutOnCooldownAction : AbilityAction
    {
        [Required] [SerializeField] private AbilityCooldownDataHolder abilityCooldownDataHolder = null;
        [Required] [SerializeField] private ICooldownable cooldown = null;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Put the item on cooldown.
            abilityCooldownDataHolder.PutOnCooldown(cooldown);

            yield return null;
        }
    }
}