using Hel.Abilities;
using System;
using UnityEngine.Events;

namespace Hel.Events.UnityEvents
{
    [Serializable] public class UnityChannelableEvent : UnityEvent<IChannelable> { }
}