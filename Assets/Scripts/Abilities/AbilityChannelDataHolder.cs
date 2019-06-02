using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Abilities
{
    /// <summary>
    /// Used to handle channelling timing and events.
    /// </summary>
    [CreateAssetMenu(fileName = "New Ability Channel Data Holder", menuName = "Abilities/Ability Channel Data Holder")]
    public class AbilityChannelDataHolder : SerializedScriptableObject
    {
        [Required] [SerializeField] private ChannelableEvent onStartedChanneling = null;
        [Required] [SerializeField] private ChannelableEvent onFinishedChanneling = null;

        public IChannelable CurrentChannelable { get; private set; } = null;
        public float RemainingChannelTime { get; private set; } = 0f;
        public bool FinishedChanneling
        {
            get
            {
                return RemainingChannelTime <= 0f;
            }
        }

        public void Tick()
        {
            //Make sure there is something to channel.
            if (CurrentChannelable == null) { return; }

            //Decrement the channel time.
            RemainingChannelTime -= Time.deltaTime;

            //Check if the channelling has finished.
            if (RemainingChannelTime <= 0f)
            {
                //Alert any listeners that we have finished channeling.
                onFinishedChanneling.Raise(CurrentChannelable);

                Finish();
            }
        }

        public void Finish()
        {
            //Reset the channel data.
            CurrentChannelable = null;
            RemainingChannelTime = 0f;
        }

        public bool StartChanneling(IChannelable channelable)
        {
            //Make sure we aren't currently channelling anything.
            if (CurrentChannelable != null) { return false; }

            //Initialise the channel data.
            CurrentChannelable = channelable;
            RemainingChannelTime = CurrentChannelable.ChannelDuration;

            //Alert any listeners that we have started channeling.
            onStartedChanneling.Raise(CurrentChannelable);

            //We successfully started channelling.
            return true;
        }
    }
}
