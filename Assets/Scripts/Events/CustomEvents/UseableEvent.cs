using Hel.Items;
using UnityEngine;

namespace Hel.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Useable Event", menuName = "Game Events/Useable Event")]
    public class UseableEvent : BaseGameEvent<IUseable> { }
}
