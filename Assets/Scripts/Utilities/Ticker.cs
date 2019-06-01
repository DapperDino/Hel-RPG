using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Utilities
{
    public class Ticker : SerializedMonoBehaviour
    {
        [Required] [SerializeField] private List<ITickable> tickables = new List<ITickable>();

        private void Update() { foreach (ITickable tickable in tickables) { tickable.Tick(); } }
    }
}
