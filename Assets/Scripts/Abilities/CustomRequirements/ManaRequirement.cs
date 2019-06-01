using Hel.Player;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Hel.Abilities.CustomRequirements
{
    [Serializable]
    public class ManaRequirement : AbilityRequirement
    {
        [Required] [SerializeField] private PlayerStatsHolder playerStatsHolder;
        [SerializeField] private int requiredMana;

        public override bool IsMet() => playerStatsHolder.Mana >= requiredMana;

        public override string GetDisplayText() => $"Mana Cost: {requiredMana}";
    }
}

