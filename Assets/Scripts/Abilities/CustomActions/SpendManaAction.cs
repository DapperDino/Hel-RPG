using Hel.Player;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class SpendManaAction : AbilityAction
    {
        [Required] [SerializeField] private PlayerStatsHolder playerStatsHolder;
        [SerializeField] private int manaToSpend;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            playerStatsHolder.Mana -= manaToSpend;
            yield return null;
        }
    }
}