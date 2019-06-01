using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    [Serializable]
    public class ChannelAction : AbilityAction
    {
        [Required] [SerializeField] private AbilityChannelSystem abilityChannelSystem;
        [Required] [SerializeField] private ChannelableEvent onChannelableInterrupted;
        [Required] [SerializeField] private IChannelable channelable;
        [SerializeField] private bool waitForChannel = true;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            if (!abilityChannelSystem.StartChanneling(channelable))
            {
                abilityCastData.AbilityActionHandler.Interrupt();
            }

            if (waitForChannel)
            {
                yield return new WaitUntil(() => abilityChannelSystem.FinishedChanneling);
            }
        }

        public override void Interrupt(AbilityCastData abilityCastData)
        {
            onChannelableInterrupted.Raise(channelable);
        }
    }
}