using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Items.Inventories
{
    /// <summary>
    /// Handles logic involved when dragging from an inventory slot.
    /// </summary>
    public class InventoryItemDragHandler : ItemDragHandler
    {
        [Required] [SerializeField] private ItemDestroyer itemDestroyer = null;
        public override void OnPointerUp(PointerEventData eventData)
        {
            //Check whether it was the left mouse button that was released.
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //Handle the base logic.
                base.OnPointerUp(eventData);

                //Make sure that the cursor is not currently over any UI.
                if (eventData.hovered.Count <= 0)
                {
                    //Activate the item destroyer with this item slot's data.
                    InventorySlot thisSlot = itemSlotUI as InventorySlot;
                    itemDestroyer.Activate(thisSlot.ItemSlot.item, thisSlot.SlotIndex);
                }
            }
        }
    }
}