using Hel.Combat;
using Hel.Items.Inventories;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Items
{
    /// <summary>
    /// Stores and handles all of an entity's items.
    /// </summary>
    [Serializable]
    public class ItemHolder
    {
        [SerializeField] private ItemSlot[] itemSlots = new ItemSlot[0];

        public Action OnItemsUpdated = delegate { };

        public ItemSlot[] ItemSlots { get { return itemSlots; } }

        public ItemHolder(int size) => itemSlots = new ItemSlot[size];

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            //Loop through each item slot.
            for (int i = 0; i < itemSlots.Length; i++)
            {
                //Make sure the slot has an item.
                if (itemSlots[i].item != null)
                {
                    //Make sure the slot's item is the same as the slot being added.
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        //Calculate the remaining space in the slot.
                        int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                        //Make sure there is enough space to add all of the new slot.
                        if (itemSlot.quantity <= slotRemainingSpace)
                        {
                            //Add all of the new slot's quantity.
                            itemSlots[i].quantity += itemSlot.quantity;

                            //Clear the new slot's quantity.
                            itemSlot.quantity = 0;

                            //Alert any listeners that the items have been updated.
                            OnItemsUpdated.Invoke();

                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            //Add the remaining space to the slot.
                            itemSlots[i].quantity += slotRemainingSpace;

                            //Remove the remaining space from the new slot.
                            itemSlot.quantity -= slotRemainingSpace;
                        }
                    }
                }
            }

            //Loop through each item slot.
            for (int i = 0; i < itemSlots.Length; i++)
            {
                //Make sure the slot doesn't have an item.
                if (itemSlots[i].item == null)
                {
                    //Make sure the quantity we are adding is less than the item's max stack value.
                    if (itemSlot.quantity <= itemSlot.item.MaxStack)
                    {
                        //Set the slot to be the slot we are adding.
                        itemSlots[i] = itemSlot;

                        //Clear the added slot's quantity.
                        itemSlot.quantity = 0;

                        //Alert any listeners that the items have been updated.
                        OnItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else
                    {
                        //Create a new item slot instance because we are adding more than the max stack.
                        ItemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);

                        //Subract the max stack from the added slot's quantity.
                        itemSlot.quantity -= itemSlot.item.MaxStack;
                    }
                }
            }

            //Alert any listeners that the items have been updated.
            OnItemsUpdated.Invoke();

            return itemSlot;
        }

        public void RemoveItem(InventoryItem itemToRemove, int quantityToRemove = 1)
        {
            //Loop through each item slot.
            for (int i = 0; i < itemSlots.Length; i++)
            {
                //Make sure the slot has an item.
                if (itemSlots[i].item != null)
                {
                    //Make sure the slot's item is the item to remove.
                    if (itemSlots[i].item == itemToRemove)
                    {
                        //Check whether the slot has a lower quantity than the amount we are trying to remove.
                        if (itemSlots[i].quantity < quantityToRemove)
                        {
                            //Update the quantity to remove by how much we actually removed.
                            quantityToRemove -= itemSlots[i].quantity;

                            //Clear the item slot.
                            itemSlots[i] = new ItemSlot();
                        }
                        else
                        {
                            //Remove the quantity to remove from the item slot.
                            itemSlots[i].quantity -= quantityToRemove;

                            //Check whether the slot is empty.
                            if (itemSlots[i].quantity == 0)
                            {
                                //Clear the slot.
                                itemSlots[i] = new ItemSlot();

                                //Alert any listeners that the items have been updated.
                                OnItemsUpdated.Invoke();

                                return;
                            }
                        }
                    }
                }
            }

            //Alert any listeners that the items have been updated.
            OnItemsUpdated.Invoke();
        }

        public void RemoveAt(int slotIndex)
        {
            //Make sure the index is valid.
            if (slotIndex > ItemSlots.Length - 1) { return; }

            //Clear the slot at the desired index.
            ItemSlots[slotIndex] = new ItemSlot();

            //Alert any listeners that the items have been updated.
            OnItemsUpdated.Invoke();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            //Cache the two slots.
            ItemSlot firstSlot = ItemSlots[indexOne];
            ItemSlot secondSlot = ItemSlots[indexTwo];

            //If the two slots are the same, do nothing.
            if (firstSlot == secondSlot) { return; }

            //Make sure the second slot has an item.
            if (secondSlot.item != null)
            {
                //Make sure the two items are the same.
                if (firstSlot.item == secondSlot.item)
                {
                    //Calculate the remaining space in the second slot.
                    int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                    //Make sure the first slot doesn't have more than the remaining space in the second slot.
                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        //Add the quantity from the first slot to the second slot.
                        ItemSlots[indexTwo].quantity += firstSlot.quantity;

                        //Clear the first slot.
                        ItemSlots[indexOne] = new ItemSlot();

                        //Alert any listeners that the items have been updated.
                        OnItemsUpdated.Invoke();

                        return;
                    }
                }
            }

            //Swap the two slots around.
            ItemSlots[indexOne] = secondSlot;
            ItemSlots[indexTwo] = firstSlot;

            //Alert any listeners that the items have been updated.
            OnItemsUpdated.Invoke();
        }

        public bool HasItem(InventoryItem item)
        {
            foreach (ItemSlot itemSlot in ItemSlots)
            {
                if (itemSlot.item != null)
                {
                    if (itemSlot.item == item)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanAddItem(ItemSlot itemSlotToAdd)
        {
            ItemSlot[] cachedSlots = itemSlots.Clone() as ItemSlot[];

            bool success = true;

            ItemSlot addedSlot = AddItem(itemSlotToAdd);

            if (addedSlot.quantity != 0)
            {
                success = false;
            }

            itemSlots = cachedSlots;

            OnItemsUpdated.Invoke();

            return success;
        }

        public bool CanAddAllItems(List<ItemSlot> itemSlotsToAdd)
        {
            ItemSlot[] cachedSlots = itemSlots.Clone() as ItemSlot[];

            bool success = true;

            for (int i = 0; i < itemSlotsToAdd.Count; i++)
            {
                ItemSlot addedSlot = AddItem(itemSlotsToAdd[i]);

                if (addedSlot.quantity != 0)
                {
                    success = false;
                    break;
                }
            }

            itemSlots = cachedSlots;

            OnItemsUpdated.Invoke();

            return success;
        }

        public int GetTotalQuantity(InventoryItem desiredItem)
        {
            int totalCount = 0;

            foreach (ItemSlot itemSlot in ItemSlots)
            {
                if (itemSlot.item == null) { continue; }
                if (itemSlot.item != desiredItem) { continue; }

                totalCount += itemSlot.quantity;
            }

            return totalCount;
        }

        public bool HasAmmunition(AmmunitionType ammunitionType)
        {
            foreach (ItemSlot itemSlot in ItemSlots)
            {
                AmmunitionItem ammunitionItem = itemSlot.item as AmmunitionItem;

                if (ammunitionItem == null) { continue; }

                if (ammunitionItem.AmmunitionType != ammunitionType) { continue; }

                return true;
            }
            return false;
        }

        public AmmunitionData ConsumeAmmunition(AmmunitionType ammunitionType)
        {
            AmmunitionData ammunitionData = new AmmunitionData();

            foreach (ItemSlot itemSlot in ItemSlots)
            {
                AmmunitionItem ammunitionItem = itemSlot.item as AmmunitionItem;

                if (ammunitionItem == null) { continue; }

                if (ammunitionItem.AmmunitionType != ammunitionType) { continue; }

                ammunitionData.ammunitionPrefab = ammunitionItem.AmmunitionPrefab;

                RemoveItem(itemSlot.item);

                break;
            }

            return ammunitionData;
        }
    }
}
