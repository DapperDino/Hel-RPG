using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hel.Abilities
{
    /// <summary>
    /// Used to handle cooldown timing and events.
    /// </summary>
    [CreateAssetMenu(fileName = "New Ability Cooldown Data Holder", menuName = "Abilities/Ability Cooldown Data Holder")]
    public class AbilityCooldownDataHolder : SerializedScriptableObject
    {
        [Required] [SerializeField] private VoidEvent onCooldownStarted = null;

        private Dictionary<ICooldownable, float> cooldownData = new Dictionary<ICooldownable, float>();

        private static readonly float GlobalCooldown = 1f;
        private float currentGlobalCooldown = 0f;

        private bool IsGlobalCooldownActive { get { return currentGlobalCooldown > 0f; } }

        public void Tick()
        {
            //Convert the dictionary keys to an array.
            ICooldownable[] cooldownables = cooldownData.Keys.ToArray();

            //Look through all of the items.
            foreach (ICooldownable cooldownable in cooldownables)
            {
                //Decrement the item's cooldown.
                cooldownData[cooldownable] -= Time.deltaTime;

                //Check if the item's cooldown has finished.
                if (cooldownData[cooldownable] <= 0)
                {
                    //Remove the item from the list of cooldowns.
                    cooldownData.Remove(cooldownable);
                }
            }

            HandleGlobalCooldown();
        }

        private void HandleGlobalCooldown()
        {
            //Decrement the global cooldown.
            currentGlobalCooldown -= Time.deltaTime;

            //Make sure the global cooldown does not go below zero.
            if (currentGlobalCooldown < 0f)
            {
                currentGlobalCooldown = 0f;
            }
        }

        public bool IsOnCooldown(ICooldownable cooldownable)
        {
            //We consider the item being on cooldown if the global cooldown is active.
            if (IsGlobalCooldownActive) { return true; }

            return cooldownData.ContainsKey(cooldownable);
        }

        public float GetDisplayCooldown(ICooldownable cooldownable, out float maxCooldownDuration)
        {
            //Get the current cooldown value and max cooldown value.
            if (cooldownData.TryGetValue(cooldownable, out float remainingCooldownTime))
            {
                //The item is on cooldown.
                maxCooldownDuration = cooldownable.MaxCooldownDuration;
                return remainingCooldownTime;
            }
            else if (IsGlobalCooldownActive)
            {
                //The item isn't on cooldown so we use the global cooldown.
                maxCooldownDuration = GlobalCooldown;
                return currentGlobalCooldown;
            }
            else
            {
                //The item isn't on cooldown and neither is the global cooldown.
                maxCooldownDuration = 0f;
                return 0f;
            }
        }

        public void PutOnCooldown(ICooldownable cooldownable)
        {
            //Make sure that the item isn't already on cooldown.
            if (cooldownData.ContainsKey(cooldownable)) { return; }

            //Add it to the list of all cooldowns.
            cooldownData.Add(cooldownable, cooldownable.MaxCooldownDuration);

            //Reset the global cooldown.
            currentGlobalCooldown = GlobalCooldown;

            //Alert any listeners that we have started a cooldown.
            onCooldownStarted.Raise();
        }
    }
}
