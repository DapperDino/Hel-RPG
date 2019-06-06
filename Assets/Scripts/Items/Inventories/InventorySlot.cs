using Hel.Items.Hotbars;
using Hel.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Items.Inventories
{
    /// <summary>
    /// Handles logic involved with the inventory slot itself.
    /// </summary>
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        [Required] [SerializeField] private PlayerInventory playerInventory = null;
        [Required] [SerializeField] protected TextMeshProUGUI itemQuantityText = null;

        public override HotbarItem SlotItem
        {
            get { return ItemSlot.item; }
            set { }
        }

        public ItemSlot ItemSlot { get { return playerInventory.ItemHolder.ItemSlots[slotIndex]; } }

        public override void OnDrop(PointerEventData eventData)
        {
            //Make sure the dropper has the "ItemDragHandler" component.
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            if (itemDragHandler == null) { return; }

            //Make sure the dropped item is from the inventory.
            if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                //Swap this slot with the dropped item's slot.
                playerInventory.ItemHolder.Swap(itemDragHandler.ItemSlotUI.SlotIndex, slotIndex);
            }
        }

        public override void UpdateSlotUI()
        {
            //Make sure there is an item in this slot.
            if (ItemSlot.item == null)
            {
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);

            itemIconImage.sprite = ItemSlot.item.Icon;
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}
