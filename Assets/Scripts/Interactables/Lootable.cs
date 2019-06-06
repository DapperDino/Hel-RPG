using Hel.Events.CustomEvents;
using Hel.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Interactables
{
    /// <summary>
    /// Handles interaction with any lootable entity.
    /// </summary>
    public class Lootable : SerializedMonoBehaviour, IInteractable
    {
        [Required] [SerializeField] private LootableEvent onLootableStartLooting = null;

        [OdinSerialize] public List<ItemSlot> ItemSlots { get; private set; } = new List<ItemSlot>();

        public void StartHover() { }

        public void Interact()
        {
            //Alert any listeners that we have started being looted.
            onLootableStartLooting.Raise(this);
        }

        public void EndHover() { }
    }
}

