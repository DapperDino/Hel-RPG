using Hel.Items;
using Hel.Items.Hotbars;
using UnityEngine.EventSystems;

namespace Hel.Magic.Spellbooks
{
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
            if (spell == null)
            {
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
