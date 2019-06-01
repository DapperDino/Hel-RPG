using UnityEngine;

namespace Hel.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Bool Event", menuName = "Game Events/Bool Event")]
    public class BoolEvent : BaseGameEvent<bool> { }
}
