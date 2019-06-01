using Hel.Events.CustomEvents;
using Hel.Events.UnityEvents;
using Hel.Interactables;

namespace Hel.Events.Listeners
{
    public class LootableListener : BaseGameEventListener<Lootable, LootableEvent, UnityLootableEvent> { }
}
