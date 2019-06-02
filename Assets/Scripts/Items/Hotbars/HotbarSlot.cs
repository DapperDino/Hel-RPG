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
    public class HotbarSlot : ItemSlotUI, IDropHandler
    {
        [Required] [SerializeField] private PlayerInventory playerInventory;
        [Required] [SerializeField] private AbilityCooldownDataHolder abilityCooldownDataHolder;
        [Required] [SerializeField] private TextMeshProUGUI itemQuantityText;
        [Required] [SerializeField] private Image cooldownOverlay;
        [Required] [SerializeField] private TextMeshProUGUI cooldownOverlayText;

        private HotbarItem slotItem;
        private Coroutine cooldownUICoroutine;

        public override HotbarItem SlotItem
        {
            get { return slotItem; }
            set { slotItem = value; UpdateSlotUI(); }
        }

        public bool AddItem(HotbarItem itemToAdd)
        {
            if (SlotItem != null) { return false; }

            SlotItem = itemToAdd;

            return true;
        }

        public void UseSlot(int index)
        {
            if (index != SlotIndex) { return; }

            if (SlotItem is ICooldownable cooldown) { abilityCooldownDataHolder.IsOnCooldown(cooldown); }

            if (SlotItem is IUseable useable) { useable.Use(); }
        }

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) { return; }

            InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
            if (inventorySlot != null)
            {
                SlotItem = inventorySlot.ItemSlot.item;
                return;
            }

            SpellSlot spellSlot = itemDragHandler.ItemSlotUI as SpellSlot;
            if (spellSlot != null)
            {
                SlotItem = spellSlot.SlotItem;
                return;
            }

            HotbarSlot hotbarSlot = itemDragHandler.ItemSlotUI as HotbarSlot;
            if (hotbarSlot != null)
            {
                HotbarItem oldItem = SlotItem;
                SlotItem = hotbarSlot.SlotItem;
                hotbarSlot.SlotItem = oldItem;
                return;
            }
        }

        public override void UpdateSlotUI()
        {
            if (SlotItem == null)
            {
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
            if (SlotItem is InventoryItem inventoryItem)
            {
                if (playerInventory.ItemHolder.HasItem(inventoryItem))
                {
                    int quantityCount = playerInventory.ItemHolder.GetTotalCount(inventoryItem);
                    itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";
                }
                else
                {
                    SlotItem = null;
                }
            }
            else
            {
                itemQuantityText.enabled = false;
            }
        }

        private void SetItemCooldownUI()
        {
            if (cooldownUICoroutine != null)
            {
                StopCoroutine(cooldownUICoroutine);
            }

            if (SlotItem is ICooldownable cooldown)
            {
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
                cooldownOverlay.enabled = false;
                cooldownOverlayText.enabled = false;
            }
        }

        private IEnumerator CooldownUiCoroutine(ICooldownable cooldown)
        {
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
