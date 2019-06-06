using Hel.Items.Inventories;
using Hel.Player;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to remove an item from the player's inventory.
    /// </summary>
    [Serializable]
    public class RemoveItemAction : AbilityAction
    {
        [Required] [SerializeField] private PlayerInventory playerInventory = null;
        [Required] [SerializeField] private InventoryItem itemToRemove = null;
        [MinValue(1)] [SerializeField] private int quantityToRemove = 1;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Remove the desired quantity of the desired item from the player's inventory.
            playerInventory.ItemHolder.RemoveItem(itemToRemove, quantityToRemove);

            yield return null;
        }
    }
}