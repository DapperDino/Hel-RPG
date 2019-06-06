using Hel.Items.Inventories;
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
        public int quantity;

        public ItemSlot(InventoryItem item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
}
