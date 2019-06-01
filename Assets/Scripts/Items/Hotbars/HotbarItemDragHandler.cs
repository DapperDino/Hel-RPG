using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Items.Hotbars
{
    public class HotbarItemDragHandler : ItemDragHandler
    {
        [Required] [SerializeField] private GameObject hotbarPanel;
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) { return; }

            base.OnPointerUp(eventData);

            if (!eventData.hovered.Contains(hotbarPanel))
            {
                (itemSlotUI as HotbarSlot).SlotItem = null;
            }
        }
    }
}