using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hel.Menus
{
    /// <summary>
    /// Used to handle pausing.
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        public static bool IsPaused { get; private set; } = false;

        [Required] [SerializeField] private GameObject pauseCanvas = null;

        private void Update()
        {
            CheckForPauseInput();
        }

        private void CheckForPauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            Time.timeScale = IsPaused ? 1 : 0;
            pauseCanvas.SetActive(!IsPaused);
            IsPaused = pauseCanvas.activeSelf;
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
