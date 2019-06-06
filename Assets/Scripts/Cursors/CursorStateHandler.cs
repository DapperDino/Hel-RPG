using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Cursors
{
    /// <summary>
    /// Used to handle input blocking and unblocking.
    /// </summary>
    public class CursorStateHandler : MonoBehaviour
    {
        [Required] [SerializeField] private VoidEvent onAllCursorBlockersDisabled = null;

        private int cursorBlockers = 0;

        private void Start() => SetCursorState();

        public void AddBlocker(bool add)
        {
            cursorBlockers += add ? 1 : -1;

            SetCursorState();
        }

        private void SetCursorState()
        {
            //Lock or free the cursor based on the number of active cursor blockers.
            Cursor.lockState = cursorBlockers >= 1 ? CursorLockMode.None : CursorLockMode.Locked;

            //Hide or show the cursor based on the number of active cursor blockers.
            Cursor.visible = cursorBlockers >= 1;

            //If there are no more cursor blockers then alert any listeners.
            if(cursorBlockers <= 0)
            {
                onAllCursorBlockersDisabled.Raise();
            }
        }
    }
}
