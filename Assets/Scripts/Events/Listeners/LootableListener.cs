using Hel.Events.CustomEvents;
using Hel.Events.UnityEvents;
using Hel.Items.Lootables;

namespace Hel.Events.Listeners
{
    public class LootableListener : BaseGameEventListener<Lootable, LootableEvent, UnityLootableEvent> { }
}
