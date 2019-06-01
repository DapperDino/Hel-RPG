using Hel.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Hel.Items.Inventories
{
    public class ItemDestroyer : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerInventory playerInventory;
        [Required] [SerializeField] private TextMeshProUGUI areYouSureText;

        private int slotIndex;

        private void OnDisable()
        {
            slotIndex = -1;
        }

        public void Activate(InventoryItem item, int slotIndex)
        {
            this.slotIndex = slotIndex;

            areYouSureText.text = $"Are you sure you wish to destroy {item.ColouredName}?";

            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            playerInventory.ItemHolder.RemoveAt(slotIndex);
            gameObject.SetActive(false);
        }
    }
}
