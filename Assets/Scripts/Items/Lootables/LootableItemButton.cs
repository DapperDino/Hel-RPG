using Hel.Events.CustomEvents;
using Hel.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hel.Items.Lootables
{
    public class LootableItemButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Required] [SerializeField] private TextMeshProUGUI itemNameText;
        [Required] [SerializeField] private Image itemIconImage;
        [Required] [SerializeField] private TextMeshProUGUI itemQuantityText;
        [Required] [SerializeField] private PlayerInventory playerInventory;
        [Required] [SerializeField] private HotbarItemEvent onMouseStartHoverItem;
        [Required] [SerializeField] private VoidEvent onMouseEndHoverItem;

        private LootableItemsDisplayer lootableItemsDisplayer;
        private ItemSlot itemSlot;

        private void OnDisable()
        {
            onMouseEndHoverItem.Raise();
        }

        public void Initialise(LootableItemsDisplayer lootableItemsDisplayer, ItemSlot itemSlot)
        {
            this.lootableItemsDisplayer = lootableItemsDisplayer;
            this.itemSlot = itemSlot;

            UpdateUI();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Loot();
        }

        public void Loot()
        {
            itemSlot = playerInventory.ItemHolder.AddItem(itemSlot);

            if (itemSlot.quantity <= 0)
            {
                Destroy(gameObject);
            }

            lootableItemsDisplayer.UpdateItemSlot(itemSlot);

            UpdateUI();
        }

        private void UpdateUI()
        {
            itemNameText.text = itemSlot.item.ColouredName;
            itemIconImage.sprite = itemSlot.item.Icon;
            itemQuantityText.text = itemSlot.quantity.ToString();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseStartHoverItem.Raise(itemSlot.item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseEndHoverItem.Raise();
        }
    }
}
