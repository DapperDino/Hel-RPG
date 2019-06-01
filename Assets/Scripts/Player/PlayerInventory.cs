using Hel.Events.CustomEvents;
using Hel.Items;
using Hel.SavingLoading;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Player
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Player/Inventory")]
    public class PlayerInventory : ScriptableObject, ISaveable
    {
        [Header("Events")]
        [Required] [SerializeField] private VoidEvent onInventoryItemsUpdated;

        public int LoadPriority { get { return 1000; } }

        public ItemHolder ItemHolder { get; } = new ItemHolder(20);

        public void OnEnable()
        {
            ItemHolder.OnItemsUpdated += onInventoryItemsUpdated.Raise;
        }

        public void OnDisable()
        {
            ItemHolder.OnItemsUpdated -= onInventoryItemsUpdated.Raise;
        }

        public void Save()
        {
            GameSaveHandler.SaveFile("player_inventory", ItemHolder);
        }

        public void Load()
        {
            GameSaveHandler.LoadFile("player_inventory", ItemHolder);
            onInventoryItemsUpdated.Raise();
        }
    }
}
