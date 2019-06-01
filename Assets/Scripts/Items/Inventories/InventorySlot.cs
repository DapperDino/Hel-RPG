using Hel.Items.Hotbars;
using Hel.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Items.Inventories
{
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        [Required] [SerializeField] private PlayerInventory playerInventory;
        [Required] [SerializeField] protected TextMeshProUGUI itemQuantityText;

        public override HotbarItem SlotItem
        {
            get { return ItemSlot.item; }
            set { }
        }

        public ItemSlot ItemSlot { get { return playerInventory.ItemHolder.ItemSlots[slotIndex]; } }

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) { return; }

            if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                playerInventory.ItemHolder.Swap(itemDragHandler.ItemSlotUI.SlotIndex, slotIndex);
            }
        }

        public override void UpdateSlotUI()
        {
            ItemSlot thisSlot = playerInventory.ItemHolder.ItemSlots[slotIndex];

            if (thisSlot.item == null)
            {
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);

            itemIconImage.sprite = thisSlot.item.Icon;
            itemQuantityText.text = thisSlot.quantity > 1 ? thisSlot.quantity.ToString() : "";
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}
