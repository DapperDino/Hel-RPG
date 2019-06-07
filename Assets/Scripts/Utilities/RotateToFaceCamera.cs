using UnityEngine;

namespace Hel.Utilities
{
    /// <summary>
    /// Used to rotate to face the camera for a billboard effect.
    /// </summary>
    public class RotateToFaceCamera : MonoBehaviour
    {
        private Transform mainCameraTransform;

        private void Start() => mainCameraTransform = Camera.main.transform;

        private void LateUpdate() => transform.LookAt(mainCameraTransform);
    }
}
