using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Items.Lootables
{
    /// <summary>
    /// Used as an interface to the contents of a lootable entity.
    /// </summary>
    public class LootableItemsDisplayer : MonoBehaviour
    {
        [Required] [SerializeField] private GameObject lootableButtonPrefab = null;
        [Required] [SerializeField] private GameObject lootableItemsDisplay = null;
        [Required] [SerializeField] private Transform lootableButtonsHolder = null;

        private Lootable lootable = null;

        public void DisplayItems(Lootable lootable)
        {
            //Loop through all of the item slots.
            foreach (ItemSlot itemSlot in lootable.ItemSlots)
            {
                //Spawn a button and initialise it with the slot's data.
                GameObject lootablebutton = Instantiate(lootableButtonPrefab, lootableButtonsHolder);
                lootablebutton.GetComponent<LootableItemButton>().Initialise(this, itemSlot);
            }

            //Enable the display panel.
            lootableItemsDisplay.SetActive(true);

            //Cache the lootable entity.
            this.lootable = lootable;
        }

        public void LootAll()
        {
            //Loop through each loot button.
            foreach (Transform child in lootableButtonsHolder)
            {
                //Attempt to loot the button.
                child.GetComponent<LootableItemButton>().Loot();
            }

            if (lootable.ItemSlots.Count <= 0)
            {
                Close();
            }
        }

        public void Close()
        {
            //Disable the display panel.
            lootableItemsDisplay.SetActive(false);

            //Loop through each loot button.
            foreach (Transform child in lootableButtonsHolder)
            {
                //Destroy the button.
                Destroy(child.gameObject);
            }
        }

        public void UpdateItemSlot(ItemSlot newSlot)
        {
            //Loop through each item slot in the lootable entity.
            for (int i = 0; i < lootable.ItemSlots.Count; i++)
            {
                //Make sure the slot is the slot we wish to update.
                if (lootable.ItemSlots[i].item == newSlot.item)
                {
                    //Overwrite the slot data.
                    lootable.ItemSlots[i] = newSlot;

                    //Handle removal of the slot if it is now empty.
                    if (newSlot.quantity <= 0)
                    {
                        lootable.ItemSlots.Remove(newSlot);
                    }

                    return;
                }
            }
        }
    }
}

