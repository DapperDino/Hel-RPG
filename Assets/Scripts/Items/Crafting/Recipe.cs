using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Items.Crafting
{
    /// <summary>
    /// Used to store data about different recipes.
    /// </summary>
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Crafting/Recipe")]
    public class Recipe : ScriptableObject
    {
        [Required] [SerializeField] private List<ItemSlot> materials = new List<ItemSlot>();
        [Required] [SerializeField] private List<ItemSlot> results = new List<ItemSlot>();

        public bool CanCraft(ItemHolder itemHolder)
        {
            //Loop through each material.
            foreach (ItemSlot itemSlot in materials)
            {
                //Make sure we have enough of the material in the inventory.
                if (itemHolder.GetTotalQuantity(itemSlot.item) < itemSlot.quantity)
                {
                    return false;
                }
            }

            //Make sure there is enough space in the inventory to add all the results.
            if (!itemHolder.CanAddAllItems(results)) { return false; }

            return true;
        }

        public void Craft(ItemHolder itemHolder)
        {
            //Make sure we are able complete the crafting.
            if (!CanCraft(itemHolder)) { return; }

            //Loop through each material.
            foreach (ItemSlot itemSlot in materials)
            {
                //Remove the item from the inventory.
                itemHolder.RemoveItem(itemSlot.item, itemSlot.quantity);
            }

            //Loop through each result.
            foreach (ItemSlot itemSlot in results)
            {
                //Add the item to the inventory.
                itemHolder.AddItem(itemSlot);
            }
        }
    }
}

