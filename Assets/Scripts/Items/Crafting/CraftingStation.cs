using Hel.Events.CustomEvents;
using Hel.Interactables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Items.Crafting
{
    /// <summary>
    /// Handles interaction with any entity that can initiate crafting.
    /// </summary>
    class CraftingStation : MonoBehaviour, IInteractable
    {
        [Required] [SerializeField] private VoidEvent onCraftingStationStartedCrafting = null;

        public string InteractionText => "Craft";

        public void Interact()
        {
            //Alert any listeners that we have started crafting.
            onCraftingStationStartedCrafting.Raise();
        }
    }
}
