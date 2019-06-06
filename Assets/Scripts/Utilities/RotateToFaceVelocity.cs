using UnityEngine;

namespace Hel.Utilities
{
    /// <summary>
    /// Used to rotate to face the direction of movement.
    /// </summary>
    public class RotateToFaceVelocity : MonoBehaviour
    {
        private Rigidbody rb = null;

        private void Start() => rb = GetComponent<Rigidbody>();

        private void Update()
        {
            //Make sure we have a rigidbody.
            if (rb == null) { return; }

            //Rotate to face in the direction of movement.
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
}

