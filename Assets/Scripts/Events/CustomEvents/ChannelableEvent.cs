using Hel.Abilities;
using UnityEngine;

namespace Hel.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Channelable Event", menuName = "Game Events/Channelable Event")]
    public class ChannelableEvent : BaseGameEvent<IChannelable> { }
}
