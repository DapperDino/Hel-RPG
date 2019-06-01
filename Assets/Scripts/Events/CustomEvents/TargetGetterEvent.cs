using Hel.Targeting;
using UnityEngine;

namespace Hel.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Target Getter Event", menuName = "Game Events/Target Getter Event")]
    public class TargetGetterEvent : BaseGameEvent<TargetGetter> { }
}
