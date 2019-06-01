using Hel.Events.CustomEvents;
using Hel.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Magic.Spellbooks
{
    public class SpellDragHandler : ItemDragHandler
    {
        [Required] [SerializeField] private HotbarItemEvent onSpellEquipPressed;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) { return; }

            base.OnPointerDown(eventData);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                onSpellEquipPressed.Raise(ItemSlotUI.SlotItem);
            }
        }
    }
}