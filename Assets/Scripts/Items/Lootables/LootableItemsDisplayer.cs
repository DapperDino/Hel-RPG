using Hel.Interactables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Items.Lootables
{
    public class LootableItemsDisplayer : MonoBehaviour
    {
        [Required] [SerializeField] private GameObject lootableButtonPrefab;
        [Required] [SerializeField] private GameObject lootableItemsDisplay;
        [Required] [SerializeField] private Transform lootableButtonsHolder;

        private Lootable lootable;

        public void DisplayItems(Lootable lootable)
        {
            if (lootable.ItemSlots.Count <= 0) { return; }

            foreach (ItemSlot itemSlot in lootable.ItemSlots)
            {
                GameObject lootablebutton = Instantiate(lootableButtonPrefab, lootableButtonsHolder);
                lootablebutton.GetComponent<LootableItemButton>().Initialise(this, itemSlot);
            }

            lootableItemsDisplay.SetActive(true);

            this.lootable = lootable;
        }

        public void LootAll()
        {
            foreach (Transform child in lootableButtonsHolder)
            {
                child.GetComponent<LootableItemButton>().Loot();
            }
        }

        public void Close()
        {
            lootableItemsDisplay.SetActive(false);

            foreach (Transform child in lootableButtonsHolder)
            {
                Destroy(child.gameObject);
            }
        }

        public void UpdateItemSlot(ItemSlot newSlot)
        {
            for (int i = 0; i < lootable.ItemSlots.Count; i++)
            {
                if (lootable.ItemSlots[i].item == newSlot.item)
                {
                    lootable.ItemSlots[i] = newSlot;

                    if (newSlot.quantity <= 0)
                    {
                        lootable.ItemSlots.Remove(newSlot);
                        if (lootable.ItemSlots.Count <= 0)
                        {
                            Close();
                        }
                    }

                    return;
                }
            }
        }
    }
}

