using Hel.Abilities;
using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Hel.Items
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
    public class ConsumableItem : InventoryItem, IUseable
    {
        [Header("Consumable Data")]
        [Required] [SerializeField] private UseableEvent onUseablePressed;
        [SerializeField] private string useText = "Does something, maybe?";

        [Header("Consumable Logic")]
        [Required] [SerializeField] private List<AbilityRequirement> abilityRequirements = new List<AbilityRequirement>();
        [Required] [SerializeField] private List<AbilityAction> abilityActions = new List<AbilityAction>();

        public List<AbilityAction> AbilityActions { get { return abilityActions; } }
        public List<AbilityRequirement> AbilityRequirements { get { return abilityRequirements; } }

        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Rarity.Name).AppendLine();
            builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
            builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

            return builder.ToString();
        }

        public void Use()
        {
            onUseablePressed.Raise(this);
        }
    }
}
