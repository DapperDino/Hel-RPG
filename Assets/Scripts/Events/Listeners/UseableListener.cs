using Hel.Events.CustomEvents;
using Hel.Events.UnityEvents;
using Hel.Items;

namespace Hel.Events.Listeners
{
    public class UseableListener : BaseGameEventListener<IUseable, UseableEvent, UnityUseableEvent> { }
}
