using Hel.Interactables;
using UnityEngine;

namespace Hel.Player
{
    public class PlayerInteractableRaycaster : MonoBehaviour
    {
        [SerializeField] private float raycastRange;
        [SerializeField] private LayerMask layerMask;

        private IInteractable currentTarget;

        public void OnDisable()
        {
            ClearCurrentTarget();
        }

        private void Update()
        {
            RaycastForInteractable();

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentTarget != null)
                {
                    currentTarget.Interact();
                }
            }
        }

        private void RaycastForInteractable()
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, transform.forward * raycastRange, Color.blue);
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastRange, layerMask))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (interactable == currentTarget)
                    {
                        return;
                    }

                    if (currentTarget != null)
                    {
                        currentTarget.EndHover();
                        currentTarget = interactable;
                        currentTarget.StartHover();
                        return;
                    }

                    currentTarget = interactable;
                    currentTarget.StartHover();
                    return;
                }
            }
            ClearCurrentTarget();
        }

        private void ClearCurrentTarget()
        {
            if (currentTarget != null)
            {
                currentTarget.EndHover();
                currentTarget = null;
            }
        }
    }
}
