using Hel.Items.Hotbars;
using Sirenix.OdinInspector;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Items
{
    /// <summary>
    /// Used to display info about items.
    /// </summary>
    public class HoverInfoPopup : MonoBehaviour
    {
        [Required] [SerializeField] private GameObject popupCanvasObject = null;
        [Required] [SerializeField] private RectTransform popupObject = null;
        [Required] [SerializeField] private TextMeshProUGUI infoText = null;
        [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f);
        [SerializeField] private float padding = 25f;

        private Canvas popupCanvas = null;

        private void Start() => popupCanvas = popupCanvasObject.GetComponent<Canvas>();

        private void Update() => FollowCursor();

        public void HideInfo() => popupCanvasObject.SetActive(false);

        private void FollowCursor()
        {
            //Make sure the popup canvas is active.
            if (!popupCanvasObject.activeSelf) { return; }

            //Calculate desired position.
            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;

            //Handle padding.
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
            //Create a string builder instance.
            StringBuilder builder = new StringBuilder();

            //Get the item's custom display text.
            builder.Append("<size=35>").Append(infoItem.ColouredName).Append("</size>\n");
            builder.Append(infoItem.GetInfoDisplayText());

            //Set info text to be displayed.
            infoText.text = builder.ToString();

            //Activate UI canvas object.
            popupCanvasObject.SetActive(true);

            //Fixes resize problem.
            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }
    }
}
