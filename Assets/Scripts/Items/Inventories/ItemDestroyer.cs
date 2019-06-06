using Hel.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Hel.Items.Inventories
{
    /// <summary>
    /// Used to destroy items that are dropped from the inventory.
    /// </summary>
    public class ItemDestroyer : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerInventory playerInventory = null;
        [Required] [SerializeField] private TextMeshProUGUI areYouSureText = null;

        private int slotIndex = 0;

        private void OnDisable() => slotIndex = -1;

        public void Activate(InventoryItem item, int slotIndex)
        {
            //Cache the slot index of item that is being destroyed.
            this.slotIndex = slotIndex;

            //Set the UI prompt text.
            areYouSureText.text = $"Are you sure you wish to destroy {item.ColouredName}?";

            //Enable this game object.
            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            //Remove the item from the cached slot index.
            playerInventory.ItemHolder.RemoveAt(slotIndex);

            //Disable this game object.
            gameObject.SetActive(false);
        }
    }
}
