using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class PutOnCooldownAction : AbilityAction
    {
        [Required] [SerializeField] private AbilityCooldownSystem cooldownDataHolder;
        [Required] [SerializeField] private ICooldownable cooldown;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            cooldownDataHolder.PutOnCooldown(cooldown);
            yield return null;
        }
    }
}