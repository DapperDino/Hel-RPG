using Hel.Player;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to spend the player's mana.
    /// </summary>
    [Serializable]
    public class SpendManaAction : AbilityAction
    {
        [Required] [SerializeField] private PlayerStatsDataHolder playerStatsDataHolder = null;
        [SerializeField] private int manaToSpend = 10;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Remove the desired amount of mana from the player's mana pool.
            playerStatsDataHolder.Mana -= manaToSpend;

            yield return null;
        }
    }
}