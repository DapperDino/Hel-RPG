using Hel.Items;
using System;
using UnityEngine.Events;

namespace Hel.Events.UnityEvents
{
    [Serializable] public class UnityUseableEvent : UnityEvent<IUseable> { }
}