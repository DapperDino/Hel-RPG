using Hel.Abilities;
using Hel.Events.CustomEvents;
using Hel.Events.UnityEvents;

namespace Hel.Events.Listeners
{
    public class ChannelableListener : BaseGameEventListener<IChannelable, ChannelableEvent, UnityChannelableEvent> { }
}
