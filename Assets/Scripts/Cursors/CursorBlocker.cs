using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Cursors
{
    /// <summary>
    /// Used to alert listeners that a cursor blocker has been enabled/disabled.
    /// </summary>
    public class CursorBlocker : MonoBehaviour
    {
        [Required] [SerializeField] private VoidEvent onCursorBlockerEnabled = null;
        [Required] [SerializeField] private VoidEvent onCursorBlockerDisabled = null;

        private void OnEnable() => onCursorBlockerEnabled.Raise();
        private void OnDisable() => onCursorBlockerDisabled.Raise();
    }
}

