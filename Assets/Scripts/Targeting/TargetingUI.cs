using Sirenix.OdinInspector;
using System.Text;
using TMPro;
using UnityEngine;

namespace Hel.Targeting
{
    public class TargetingUI : MonoBehaviour
    {
        [Required] [SerializeField] private GameObject targetingUI = null;
        [Required] [SerializeField] private TextMeshProUGUI currentTargetsText = null;

        private TargetGetter currentTargetGetter = null;

        public void DisplayTargets(TargetGetter targetGetter)
        {
            currentTargetGetter = targetGetter;
            targetingUI.SetActive(true);
        }

        public void HideTargets()
        {
            currentTargetsText.text = "";
            currentTargetGetter = null;
            targetingUI.SetActive(false);
        }

        private void Update()
        {
            if (currentTargetGetter == null) { return; }

            StringBuilder builder = new StringBuilder();

            builder.Append("Targeting: ");
            foreach (ITargetable target in currentTargetGetter.CurrentTargets)
            {
                builder.Append(target.name).Append(", ");
            }
            builder.Length -= 2;

            currentTargetsText.text = builder.ToString();
        }
    }
}
