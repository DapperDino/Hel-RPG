using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Hel.Abilities.CustomRequirements
{
    [Serializable]
    public class CooldownRequirement : AbilityRequirement
    {
        [Required] [SerializeField] private AbilityCooldownSystem cooldownDataHolder;
        [Required] [SerializeField] private ICooldownable cooldownable;

        public override bool IsMet() => !cooldownDataHolder.IsOnCooldown(cooldownable);

        public override string GetDisplayText() => $"Cooldown: {cooldownable.MaxCooldownDuration} sec";
    }
}

