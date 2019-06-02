using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Hel.Abilities.CustomActions
{
    /// <summary>
    /// Used to wait for a channel to happen before continuing with actions.
    /// </summary>
    [Serializable]
    public class ChannelAction : AbilityAction
    {
        [Required] [SerializeField] private AbilityChannelDataHolder abilityChannelDataHolder = null;
        [Required] [SerializeField] private ChannelableEvent onChannelableInterrupted = null;
        [Required] [SerializeField] private IChannelable channelable = null;
        [SerializeField] private bool waitForChannel = true;

        public override IEnumerator Trigger(AbilityCastData abilityCastData)
        {
            //Check if something is already being channeled and if not, start channeling.
            if (!abilityChannelDataHolder.StartChanneling(channelable))
            {
                //Something was already being channeled so we interrupt.
                abilityCastData.AbilityActionHandler.Interrupt();
            }

            //If the spell needs to wait until the channel has finished to continue then do so.
            if (waitForChannel)
            {
                yield return new WaitUntil(() => abilityChannelDataHolder.FinishedChanneling);
            }
        }

        public override void Interrupt(AbilityCastData abilityCastData) => onChannelableInterrupted.Raise(channelable);
    }
}