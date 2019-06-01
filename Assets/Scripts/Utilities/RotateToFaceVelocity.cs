using UnityEngine;

namespace Hel.Utilities
{
    public class RotateToFaceVelocity : MonoBehaviour
    {
        private Rigidbody rb;

        private void Start() => rb = GetComponent<Rigidbody>();

        private void Update()
        {
            if (rb == null) { return; }

            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
}

