using Hel.Combat;
using Hel.Items.Inventories;
using Sirenix.OdinInspector;
using System.Text;
using UnityEngine;

namespace Hel.Items
{
    /// <summary>
    /// Used to store data about different types of ammunition.
    /// </summary>
    [CreateAssetMenu(fileName = "New Ammunition", menuName = "Items/Ammunition")]
    public class AmmunitionItem : InventoryItem
    {
        [Header("Ammunition Data")]
        [Required] [SerializeField] private AmmunitionType ammunitionType = null;
        [Required] [InlineEditor(InlineEditorModes.LargePreview)] [SerializeField] private GameObject ammunitionPrefab = null;

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
