using Hel.Items.Hotbars;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hel.Items
{
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [Required] [SerializeField] protected Image itemIconImage;

        protected int slotIndex;

        public abstract HotbarItem SlotItem { get; set; }

        public int SlotIndex { get { return slotIndex; } }

        private void OnEnable()
        {
            UpdateSlotUI();
        }

        protected virtual void Start()
        {
            slotIndex = transform.GetSiblingIndex();
            UpdateSlotUI();
        }

        public abstract void OnDrop(PointerEventData eventData);
        public abstract void UpdateSlotUI();

        protected virtual void EnableSlotUI(bool enable)
        {
            itemIconImage.enabled = enable;
        }
    }
}
