using Hel.Items;
using Hel.Items.Hotbars;
using UnityEngine.EventSystems;

namespace Hel.Magic.Spellbooks
{
    /// <summary>
    /// Handles logic involved with the spell slot itself.
    /// </summary>
    public class SpellSlot : ItemSlotUI
    {
        private Spell spell;

        public override HotbarItem SlotItem
        {
            get { return spell; }
            set { }
        }

        public void Initialise(Spell spell)
        {
            this.spell = spell;
            UpdateSlotUI();
        }

        public override void OnDrop(PointerEventData eventData) { }

        public override void UpdateSlotUI()
        {
            //Make sure we have a spell.
            if (spell == null)
            {   //Disable our slot UI.
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);

            itemIconImage.sprite = spell.Icon;
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            gameObject.SetActive(enable);
        }
    }
}
