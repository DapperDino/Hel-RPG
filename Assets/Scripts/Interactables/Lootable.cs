using Hel.Events.CustomEvents;
using Hel.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Interactables
{
    public class Lootable : SerializedMonoBehaviour, IInteractable
    {
        [Required] [SerializeField] private LootableEvent onLootableStartLooting;

        [OdinSerialize]
        public List<ItemSlot> ItemSlots { get; private set; } = new List<ItemSlot>();

        public void StartHover() { }

        public void Interact()
        {
            onLootableStartLooting.Raise(this);
        }

        public void EndHover() { }
    }
}

