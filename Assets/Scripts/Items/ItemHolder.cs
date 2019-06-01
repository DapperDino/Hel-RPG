using Hel.Combat;
using System;
using UnityEngine;

namespace Hel.Items
{
    [Serializable]
    public class ItemHolder
    {
        [SerializeField] private ItemSlot[] itemSlots;

        public Action OnItemsUpdated = delegate { };

        public ItemSlot[] ItemSlots { get { return itemSlots; } }

        public ItemHolder(int size)
        {
            itemSlots = new ItemSlot[size];
        }

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;
                        if (itemSlot.quantity <= slotRemainingSpace)
                        {
                            itemSlots[i].quantity += itemSlot.quantity;
                            itemSlot.quantity = 0;
                            OnItemsUpdated.Invoke();
                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].quantity += slotRemainingSpace;
                            itemSlot.quantity -= slotRemainingSpace;
                        }
                    }
                }
            }

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    if (itemSlot.quantity <= itemSlot.item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;
                        itemSlot.quantity = 0;
                        OnItemsUpdated.Invoke();
                        return itemSlot;
                    }
                    else
                    {
                        ItemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);
                        itemSlot.quantity -= itemSlot.item.MaxStack;
                    }
                }
            }

            OnItemsUpdated.Invoke();
            return itemSlot;
        }

        public void RemoveItem(InventoryItem itemToRemove, int quantityToRemove = 1)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == itemToRemove)
                    {
                        if (itemSlots[i].quantity < quantityToRemove)
                        {
                            quantityToRemove -= itemSlots[i].quantity;
                            itemSlots[i] = new ItemSlot();
                        }
                        else
                        {
                            itemSlots[i].quantity -= quantityToRemove;
                            if (itemSlots[i].quantity < 1)
                            {
                                itemSlots[i] = new ItemSlot();
                                OnItemsUpdated.Invoke();
                                return;
                            }
                        }
                    }
                }
            }
            OnItemsUpdated.Invoke();
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex > ItemSlots.Length - 1) { return; }

            ItemSlots[slotIndex] = new ItemSlot();
            OnItemsUpdated.Invoke();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            ItemSlot firstSlot = ItemSlots[indexOne];
            ItemSlot secondSlot = ItemSlots[indexTwo];

            if (secondSlot.item != null)
            {
                if (firstSlot.item == secondSlot.item)
                {
                    int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;
                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        ItemSlots[indexTwo].quantity += firstSlot.quantity;
                        ItemSlots[indexOne] = new ItemSlot();
                        OnItemsUpdated.Invoke();
                        return;
                    }
                }
            }

            ItemSlots[indexOne] = secondSlot;
            ItemSlots[indexTwo] = firstSlot;
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

        public int GetTotalCount(InventoryItem desiredItem)
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

            foreach(ItemSlot itemSlot in ItemSlots)
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
