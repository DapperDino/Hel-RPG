using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Items
{
    /// <summary>
    /// Base class for all drag handlers.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Required] [SerializeField] protected ItemSlotUI itemSlotUI = null;
        [Required] [SerializeField] protected HotbarItemEvent onMouseStartHoverItem = null;
        [Required] [SerializeField] protected VoidEvent onMouseEndHoverItem = null;

        private CanvasGroup canvasGroup = null;
        private Transform originalParent = null;
        private bool isHovering = false;

        public ItemSlotUI ItemSlotUI { get { return itemSlotUI; } }

        protected virtual void Start() => canvasGroup = GetComponent<CanvasGroup>();

        private void OnDisable()
        {
            //End hovering if we are when disabled.
            if (isHovering)
            {
                onMouseEndHoverItem.Raise();
                isHovering = false;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            //Check whether it was the left mouse button that was pressed.
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //Alert any listeners that we have stopped hovering.
                onMouseEndHoverItem.Raise();

                //Cache original parent.
                originalParent = transform.parent;

                //Set new parent.
                transform.SetParent(transform.parent.parent);

                //Stop blocking raycasts (so we can drop on another slot).
                canvasGroup.blocksRaycasts = false;
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            //Check whether it is the left mouse button that is being held.
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //Follow the cursor.
                transform.position = Input.mousePosition;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            //Check whether it was the left mouse button that was released.
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //Reset.
                transform.SetParent(originalParent);
                transform.localPosition = Vector3.zero;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Alert any listeners that we have started hovering.
            onMouseStartHoverItem.Raise(ItemSlotUI.SlotItem);
            isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Alert any listeners that we have stopped hovering.
            onMouseEndHoverItem.Raise();
            isHovering = false;
        }
    }
}
