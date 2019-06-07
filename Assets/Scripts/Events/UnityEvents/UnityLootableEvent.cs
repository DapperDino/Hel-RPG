using Hel.Items.Lootables;
using System;
using UnityEngine.Events;

namespace Hel.Events.UnityEvents
{
    [Serializable] public class UnityLootableEvent : UnityEvent<Lootable> { }
}