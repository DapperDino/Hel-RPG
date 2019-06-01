using UnityEngine;

namespace Hel.OnContact
{
    [RequireComponent(typeof(Collider))]
    public class StickOnContact : MonoBehaviour
    {
        private Rigidbody rb;
        private new Collider collider;
        private Transform stickAnchor;
        private bool hasStuck = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

        private void Update()
        {
            if (stickAnchor != null)
            {
                transform.position = stickAnchor.position;
                transform.rotation = stickAnchor.rotation;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (hasStuck) { return; }

            hasStuck = true;

            transform.position = collision.contacts[0].point;

            collider.isTrigger = true;

            GameObject anchor = new GameObject("Anchor");
            anchor.transform.position = transform.position;
            anchor.transform.rotation = transform.rotation;
            anchor.transform.parent = collision.transform;

            stickAnchor = anchor.transform;

            Destroy(rb);
        }
    }
}
