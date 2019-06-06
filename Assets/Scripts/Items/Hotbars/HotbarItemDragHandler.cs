using UnityEngine.EventSystems;

namespace Hel.Items.Hotbars
{
    /// <summary>
    /// Handles logic involved when dragging from a hotbar slot.
    /// </summary>
    public class HotbarItemDragHandler : ItemDragHandler
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            //Check whether it was the left mouse button that was released.
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //Handle the base logic.
                base.OnPointerUp(eventData);

                //Make sure that the cursor is not currently over any UI.
                if (eventData.hovered.Count == 0)
                {
                    //Clear the slot.
                    (itemSlotUI as HotbarSlot).SlotItem = null;
                }
            }
        }
    }
}