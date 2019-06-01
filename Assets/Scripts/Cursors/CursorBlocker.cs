using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Cursors
{
    public class CursorBlocker : MonoBehaviour
    {
        [Required] [SerializeField] private VoidEvent onCursorBlockerEnabled;
        [Required] [SerializeField] private VoidEvent onCursorBlockerDisabled;

        private void OnEnable() => onCursorBlockerEnabled.Raise();
        private void OnDisable() => onCursorBlockerDisabled.Raise();
    }
}

