using Hel.Events.CustomEvents;
using Hel.Utilities;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hel.Abilities
{
    [CreateAssetMenu(fileName = "New Ability Cooldown System", menuName = "Player/Ability Cooldown System")]
    public class AbilityCooldownSystem : SerializedScriptableObject, ITickable
    {
        [Required] [SerializeField] private VoidEvent onCooldownStarted;

        private Dictionary<ICooldownable, float> cooldownData = new Dictionary<ICooldownable, float>();

        private static readonly float GlobalCooldown = 1f;
        private float currentGlobalCooldown = 0f;

        public void Tick()
        {
            ICooldownable[] cooldownables = cooldownData.Keys.ToArray();

            foreach (ICooldownable cooldownable in cooldownables)
            {
                cooldownData[cooldownable] -= Time.deltaTime;

                if (cooldownData[cooldownable] <= 0)
                {
                    cooldownData.Remove(cooldownable);
                }
            }

            currentGlobalCooldown -= Time.deltaTime;
            if (currentGlobalCooldown < 0f)
            {
                currentGlobalCooldown = 0f;
            }
        }

        public bool IsOnCooldown(ICooldownable cooldownable)
        {
            if (currentGlobalCooldown > 0f) { return true; }

            return cooldownData.ContainsKey(cooldownable);
        }

        public float GetDisplayCooldown(ICooldownable cooldownable, out float maxCooldownDuration)
        {
            if (cooldownData.TryGetValue(cooldownable, out float remainingCooldownTime)) { maxCooldownDuration = cooldownable.MaxCooldownDuration; return remainingCooldownTime; }
            else if (currentGlobalCooldown > 0f) { maxCooldownDuration = GlobalCooldown; return currentGlobalCooldown; }
            else { maxCooldownDuration = 0f; return 0f; }
        }

        public void PutOnCooldown(ICooldownable cooldownable)
        {
            if (cooldownData.ContainsKey(cooldownable)) { return; }

            if (cooldownable.MaxCooldownDuration > GlobalCooldown)
            {
                cooldownData.Add(cooldownable, cooldownable.MaxCooldownDuration);
            }

            currentGlobalCooldown = GlobalCooldown;

            onCooldownStarted.Raise();
        }
    }
}
