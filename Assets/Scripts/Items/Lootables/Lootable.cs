using Hel.Events.CustomEvents;
using Hel.Interactables;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Items.Lootables
{
    /// <summary>
    /// Handles interaction with any lootable entity.
    /// </summary>
    public class Lootable : MonoBehaviour, IInteractable
    {
        [Required] [SerializeField] private LootableEvent onLootableStartLooting = null;
        [Required] [SerializeField] private List<ItemSlot> itemSlots = new List<ItemSlot>();

        public string InteractionText => "Loot";
        public List<ItemSlot> ItemSlots => itemSlots;

        public void Interact()
        {
            //Alert any listeners that we have started being looted.
            onLootableStartLooting.Raise(this);
        }
    }
}

