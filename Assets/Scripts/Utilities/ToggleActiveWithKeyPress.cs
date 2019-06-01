using Hel.Menus;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Utilities
{
    public class ToggleActiveWithKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode toggleKey;
        [Required] [SerializeField] private GameObject objectToToggle;

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                if (PauseMenu.IsPaused) { return; }

                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
        }
    }
}
