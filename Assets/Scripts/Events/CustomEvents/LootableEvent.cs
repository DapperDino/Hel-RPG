using Hel.Items.Lootables;
using UnityEngine;

namespace Hel.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Lootable Event", menuName = "Game Events/Lootable Event")]
    public class LootableEvent : BaseGameEvent<Lootable> { }
}
