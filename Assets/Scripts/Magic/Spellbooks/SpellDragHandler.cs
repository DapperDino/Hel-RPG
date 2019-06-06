using Hel.Events.CustomEvents;
using Hel.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Magic.Spellbooks
{
    /// <summary>
    /// Handles logic involved when dragging from a spell slot.
    /// </summary>
    public class SpellDragHandler : ItemDragHandler
    {
        [Required] [SerializeField] private HotbarItemEvent onSpellEquipPressed = null;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //Handle the base logic.
                base.OnPointerDown(eventData);

                //Check whether the left shift key is currently held down.
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //Alert any listeners that we wish to equip this spell.
                    //TODO Rename
                    onSpellEquipPressed.Raise(ItemSlotUI.SlotItem);
                }
            }
        }
    }
}