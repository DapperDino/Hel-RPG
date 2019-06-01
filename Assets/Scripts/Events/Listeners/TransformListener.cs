using Hel.Events.CustomEvents;
using Hel.Events.UnityEvents;
using UnityEngine;

namespace Hel.Events.Listeners
{
    public class TransformListener : BaseGameEventListener<Transform, TransformEvent, UnityTransformEvent> { }
}
