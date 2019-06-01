using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Items.Inventories
{
    public class InventoryItemDragHandler : ItemDragHandler
    {
        [Required] [SerializeField] private GameObject inventoryPanel;
        [Required] [SerializeField] private ItemDestroyer itemDestroyer;
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) { return; }

            base.OnPointerUp(eventData);

            if (eventData.hovered.Count <= 0)
            {
                InventorySlot thisSlot = itemSlotUI as InventorySlot;
                itemDestroyer.Activate(thisSlot.ItemSlot.item, thisSlot.SlotIndex);
            }
        }
    }
}