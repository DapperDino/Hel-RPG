using UnityEngine;

namespace Hel.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Transform Event", menuName = "Game Events/Transform Event")]
    public class TransformEvent : BaseGameEvent<Transform> { }
}
