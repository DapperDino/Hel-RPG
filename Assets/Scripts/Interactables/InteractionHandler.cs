using Hel.Inputs;
using TMPro;
using UnityEngine;

namespace Hel.Interactables
{
    /// <summary>
    /// Used to handle interaction between the player and objects in the world.
    /// </summary>
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactionText = null;

        private static InteractionHandler currentInteractionHandler = null;
        private static readonly int hashFadeIn = Animator.StringToHash("FadeIn");

        private Animator animator = null;
        private IInteractable interactable = null;

        private void Start()
        {
            animator = GetComponent<Animator>();

            //Make sure we have an interactable component.
            interactable = GetComponentInParent<IInteractable>();
            if (interactable == null) { Destroy(gameObject); }

            //Set the display text for specific type of interactable.
            interactionText.text = $"[E] {interactable.InteractionText}";
        }

        private void Update()
        {
            CheckForInteraction();
        }

        private void CheckForInteraction()
        {
            //Make sure we are the current handler.
            if (currentInteractionHandler == this)
            {
                //Check if the interact key was pressed.
                if (PlayerInputManager.Interact)
                {
                    //Handle interaction logic.
                    interactable.Interact();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            animator.SetBool(hashFadeIn, true);
            currentInteractionHandler = this;
        }

        private void OnTriggerExit(Collider other)
        {
            animator.SetBool(hashFadeIn, false);
            if (currentInteractionHandler == this)
            {
                currentInteractionHandler = null;
            }
        }
    }
}
