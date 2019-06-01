using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Cursors
{
    public class CursorStateHandler : MonoBehaviour
    {
        [Required] [SerializeField] private VoidEvent onAllCursorBlockersDisabled;

        private int cursorBlockers = 0;

        private void Start() => SetCursorState();

        public void AddBlocker(bool add)
        {
            cursorBlockers += add ? 1 : -1;
            SetCursorState();
        }

        private void SetCursorState()
        {
            Cursor.lockState = cursorBlockers >= 1 ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = cursorBlockers >= 1;
            if(cursorBlockers <= 0)
            {
                onAllCursorBlockersDisabled.Raise();
            }
        }
    }
}
