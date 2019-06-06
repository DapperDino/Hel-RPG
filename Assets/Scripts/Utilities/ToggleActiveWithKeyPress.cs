using Hel.Menus;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Utilities
{
    /// <summary>
    /// Used to toggle UI with key presses.
    /// </summary>
    public class ToggleActiveWithKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode toggleKey = KeyCode.None;
        [Required] [SerializeField] private GameObject objectToToggle = null;

        private void Update()
        {
            //Check to see if the toggle key was just pressed.
            if (Input.GetKeyDown(toggleKey))
            {
                //Don't toggle if we are currently paused.
                if (PauseMenu.IsPaused) { return; }

                //Toggle the object.
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
        }
    }
}
