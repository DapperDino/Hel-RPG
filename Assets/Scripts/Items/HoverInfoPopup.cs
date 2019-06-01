using Hel.Items.Hotbars;
using Sirenix.OdinInspector;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Items
{
    public class HoverInfoPopup : MonoBehaviour
    {
        [Required] [SerializeField] private GameObject popupCanvasObject;
        [Required] [SerializeField] private RectTransform popupObject;
        [Required] [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float padding;

        private Canvas popupCanvas;

        private void Start()
        {
            popupCanvas = popupCanvasObject.GetComponent<Canvas>();
        }

        private void Update()
        {
            FollowCursor();
        }

        private void FollowCursor()
        {
            if (!popupCanvasObject.activeSelf) { return; }

            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;
            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }
            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) + padding;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }
            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }
            popupObject.transform.position = newPos;
        }

        public void DisplayInfo(HotbarItem infoItem)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<size=35>").Append(infoItem.ColouredName).Append("</size>\n");
            builder.Append(infoItem.GetInfoDisplayText());

            infoText.text = builder.ToString();

            popupCanvasObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }

        public void HideInfo()
        {
            popupCanvasObject.SetActive(false);
        }
    }
}
