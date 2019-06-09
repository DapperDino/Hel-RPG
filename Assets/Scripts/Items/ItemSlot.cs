using Hel.Items.Inventories;
using Sirenix.OdinInspector;
using System;

namespace Hel.Items
{
    /// <summary>
    /// Stores the data for an instance of an item.
    /// </summary>
    [Serializable]
    public struct ItemSlot
    {
        public InventoryItem item;
        [MinValue(1)] public int quantity;

        public ItemSlot(InventoryItem item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }

        public static bool operator ==(ItemSlot a, ItemSlot b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ItemSlot a, ItemSlot b)
        {
            return !a.Equals(b);
        }
    }
}
