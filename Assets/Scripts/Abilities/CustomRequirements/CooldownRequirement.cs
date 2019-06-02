using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Hel.Abilities.CustomRequirements
{
    /// <summary>
    /// Used to check whether a given item is on cooldown or not.
    /// </summary>
    [Serializable]
    public class CooldownRequirement : AbilityRequirement
    {
        [Required] [SerializeField] private AbilityCooldownDataHolder abilityCooldownDataHolder = null;
        [Required] [SerializeField] private ICooldownable cooldownable = null;

        public override bool IsMet() => !abilityCooldownDataHolder.IsOnCooldown(cooldownable);

        public override string GetDisplayText() => $"Cooldown: {cooldownable.MaxCooldownDuration} sec";
    }
}

