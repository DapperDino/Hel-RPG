using Hel.Events.CustomEvents;
using Hel.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Abilities
{
    [CreateAssetMenu(fileName = "New Ability Channel System", menuName = "Player/Ability Channel System")]
    public class AbilityChannelSystem : SerializedScriptableObject, ITickable
    {
        [Required] [SerializeField] private ChannelableEvent onStartedChanneling;
        [Required] [SerializeField] private ChannelableEvent onFinishedChanneling;

        public IChannelable CurrentChannelable { get; private set; } = null;
        public float RemainingChannelTime { get; private set; } = 0f;
        public bool FinishedChanneling
        {
            get
            {
                return RemainingChannelTime == 0f;
            }
        }

        public void Tick()
        {
            if (CurrentChannelable == null) { return; }

            RemainingChannelTime -= Time.deltaTime;

            if (RemainingChannelTime <= 0f)
            {
                onFinishedChanneling.Raise(CurrentChannelable);

                Finish();
            }
        }

        public void Finish()
        {
            CurrentChannelable = null;
            RemainingChannelTime = 0f;
        }

        public bool StartChanneling(IChannelable channelable)
        {
            if (CurrentChannelable != null) { return false; }

            CurrentChannelable = channelable;

            RemainingChannelTime = CurrentChannelable.ChannelDuration;

            onStartedChanneling.Raise(CurrentChannelable);

            return true;
        }
    }
}
