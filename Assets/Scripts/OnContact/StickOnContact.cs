using UnityEngine;

namespace Hel.OnContact
{
    /// <summary>
    /// Used to handle sticking on contact.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class StickOnContact : MonoBehaviour
    {
        private Rigidbody rb = null;
        private new Collider collider = null;
        private Transform stickAnchor = null;
        private bool hasStuck = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

        private void Update()
        {
            HandleAnchorTracking();
        }

        private void HandleAnchorTracking()
        {
            //Make sure there is a stick anchor.
            if (stickAnchor != null)
            {
                //Track the anchor.
                transform.position = stickAnchor.position;
                transform.rotation = stickAnchor.rotation;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Make sure we haven't already stuck
            if (hasStuck) { return; }

            //Now we have stuck.
            hasStuck = true;

            //TODO check whether this should be here.
            //transform.position = collision.contacts[0].point;

            //Stop the collider from physically hitting other objects.
            collider.isTrigger = true;

            //Create and initialise the anchor.
            GameObject anchor = new GameObject("Anchor");
            anchor.transform.position = transform.position;
            anchor.transform.rotation = transform.rotation;
            anchor.transform.parent = collision.transform;

            //Cachethe anchor
            stickAnchor = anchor.transform;

            //Remove the rigidbody component from us.
            Destroy(rb);
        }
    }
}
