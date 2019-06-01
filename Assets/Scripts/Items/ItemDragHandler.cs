using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hel.Items
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Required] [SerializeField] protected ItemSlotUI itemSlotUI;
        [Required] [SerializeField] protected HotbarItemEvent onMouseStartHoverItem;
        [Required] [SerializeField] protected VoidEvent onMouseEndHoverItem;

        private CanvasGroup canvasGroup;
        private Transform originalParent;
        private bool isHovering = false;

        public ItemSlotUI ItemSlotUI { get { return itemSlotUI; } }

        protected virtual void Start() => canvasGroup = GetComponent<CanvasGroup>();

        private void OnDisable()
        {
            if (isHovering)
            {
                onMouseEndHoverItem.Raise();
                isHovering = false;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) { return; }

            onMouseEndHoverItem.Raise();

            originalParent = transform.parent;
            transform.SetParent(transform.parent.parent);
            canvasGroup.blocksRaycasts = false;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) { return; }

            transform.position = Input.mousePosition;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) { return; }

            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseStartHoverItem.Raise(ItemSlotUI.SlotItem);
            isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseEndHoverItem.Raise();
            isHovering = false;
        }
    }
}
