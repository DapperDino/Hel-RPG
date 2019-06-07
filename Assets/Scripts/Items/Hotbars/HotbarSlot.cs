using Hel.Abilities;
using Hel.Items.Inventories;
using Hel.Magic.Spellbooks;
using Hel.Player;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hel.Items.Hotbars
{
    /// <summary>
    /// Handles logic involved with the hotbar slot itself.
    /// </summary>
    public class HotbarSlot : ItemSlotUI, IDropHandler
    {
        [Required] [SerializeField] private PlayerInventory playerInventory = null;
        [Required] [SerializeField] private AbilityCooldownDataHolder abilityCooldownDataHolder = null;
        [Required] [SerializeField] private TextMeshProUGUI itemQuantityText = null;
        [Required] [SerializeField] private Image cooldownOverlay = null;
        [Required] [SerializeField] private TextMeshProUGUI cooldownOverlayText = null;

        private HotbarItem slotItem = null;
        private Coroutine cooldownUICoroutine = null;

        public override HotbarItem SlotItem
        {
            get { return slotItem; }
            set { slotItem = value; UpdateSlotUI(); }
        }

        public bool AddItem(HotbarItem itemToAdd)
        {
            //Make sure the slot is empty.
            if (SlotItem != null) { return false; }

            //Set the slot's item to the item being added.
            SlotItem = itemToAdd;

            return true;
        }

        public void UseSlot(int index)
        {
            //Make sure this is the correct slot.
            if (index != SlotIndex) { return; }

            //Make sure the item isn't on cooldown.
            if (SlotItem is ICooldownable cooldown) { abilityCooldownDataHolder.IsOnCooldown(cooldown); }

            //If the item is useable, then use it.
            if (SlotItem is IUseable useable) { useable.Use(); }
        }

        public override void OnDrop(PointerEventData eventData)
        {
            //Make sure the dropper has the "ItemDragHandler" component.
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            if (itemDragHandler == null) { return; }

            //Make sure the dropped item is from the inventory.
            InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
            if (inventorySlot != null)
            {
                //Set our item to the dropped item.
                SlotItem = inventorySlot.ItemSlot.item;
                return;
            }

            //Make sure the dropped item is from the spellbook.
            SpellSlot spellSlot = itemDragHandler.ItemSlotUI as SpellSlot;
            if (spellSlot != null)
            {
                //Set our item to the dropped item(spell).
                SlotItem = spellSlot.SlotItem;
                return;
            }

            //Make sure the dropped item is from the hotbar.
            HotbarSlot hotbarSlot = itemDragHandler.ItemSlotUI as HotbarSlot;
            if (hotbarSlot != null)
            {
                //Swap the dropped item with our item.
                HotbarItem oldItem = SlotItem;
                SlotItem = hotbarSlot.SlotItem;
                hotbarSlot.SlotItem = oldItem;
                return;
            }
        }

        public override void UpdateSlotUI()
        {
            //Make sure we have an item.
            if (SlotItem == null)
            {
                //Disable our slot UI.
                EnableSlotUI(false);
                return;
            }

            itemIconImage.sprite = SlotItem.Icon;

            EnableSlotUI(true);

            SetItemQuantityUI();

            SetItemCooldownUI();
        }

        private void SetItemQuantityUI()
        {
            //Make sure our item is from the inventory.
            if (SlotItem is InventoryItem inventoryItem)
            {
                //Make sure the item is in the inventory.
                if (playerInventory.ItemHolder.HasItem(inventoryItem))
                {
                    //Get the quantity of the item and set the quantity text accordingly.
                    int quantityCount = playerInventory.ItemHolder.GetTotalQuantity(inventoryItem);
                    itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";
                }
                else
                {
                    //Clear the slot
                    SlotItem = null;
                }
            }
            else
            {
                //Disable the quantity text.
                itemQuantityText.enabled = false;
            }
        }

        private void SetItemCooldownUI()
        {
            //Make sure there isn't a cooldown coroutine in progress.
            if (cooldownUICoroutine != null)
            {
                StopCoroutine(cooldownUICoroutine);
            }

            //Make sure our item is cooldownable.
            if (SlotItem is ICooldownable cooldown)
            {
                //Check whether our item is on cooldown.
                if (abilityCooldownDataHolder.IsOnCooldown(cooldown))
                {
                    SetCooldownUI(cooldown);
                    cooldownUICoroutine = StartCoroutine(CooldownUiCoroutine(cooldown));
                }
                else
                {
                    ClearCooldownUI();
                }
            }
            else
            {
                //Disable the cooldown related UI for this slot.
                cooldownOverlay.enabled = false;
                cooldownOverlayText.enabled = false;
            }
        }

        private IEnumerator CooldownUiCoroutine(ICooldownable cooldown)
        {
            //Update the cooldown UI whilst our item is on cooldown.
            while (abilityCooldownDataHolder.IsOnCooldown(cooldown))
            {
                SetCooldownUI(cooldown);
                yield return null;
            }

            ClearCooldownUI();

            cooldownUICoroutine = null;
        }

        private void SetCooldownUI(ICooldownable cooldown)
        {
            float remainingCooldownTime = abilityCooldownDataHolder.GetDisplayCooldown(cooldown, out float maxCooldownDuration);
            cooldownOverlay.fillAmount = remainingCooldownTime / maxCooldownDuration;
            cooldownOverlayText.text = Mathf.RoundToInt(remainingCooldownTime).ToString();
        }

        private void ClearCooldownUI()
        {
            cooldownOverlay.fillAmount = 0f;
            cooldownOverlayText.text = "";
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
            cooldownOverlay.enabled = enable;
            cooldownOverlayText.enabled = enable;
        }
    }
}
