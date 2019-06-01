using Hel.Combat;
using Sirenix.OdinInspector;
using System.Text;
using UnityEngine;

namespace Hel.Items
{
    [CreateAssetMenu(fileName = "New Ammunition", menuName = "Items/Ammunition")]
    public class AmmunitionItem : InventoryItem
    {
        [Header("Ammunition Data")]
        [SerializeField] private AmmunitionType ammunitionType;
        [InlineEditor(InlineEditorModes.LargePreview)] [SerializeField] private GameObject ammunitionPrefab;

        public AmmunitionType AmmunitionType { get { return ammunitionType; } }
        public GameObject AmmunitionPrefab { get { return ammunitionPrefab; } }

        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Rarity.Name).AppendLine();
            builder.Append(AmmunitionType.Name).AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
            builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

            return builder.ToString();
        }
    }
}
