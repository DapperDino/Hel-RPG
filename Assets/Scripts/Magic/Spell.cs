using Hel.Abilities;
using Hel.Events.CustomEvents;
using Hel.Items;
using Hel.Items.Hotbars;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Hel.Magic
{
    /// <summary>
    /// Used to store data about different spells.
    /// </summary>
    [CreateAssetMenu(fileName = "New Spell", menuName = "Magic/Spell")]
    public class Spell : HotbarItem, IUseable, ICooldownable, IChannelable
    {
        [Header("Spell Data")]
        [SerializeField] private string description = "New Spell Description";
        [Required] [SerializeField] private UseableEvent onUseablePressed = null;
        [Required] [SerializeField] private ElementTree elementTree = null;
        [MinValue(0f)] [SerializeField] private float maxCooldownDuration = 1f;
        [MinValue(0f)] [SerializeField] private float channelDuration = 1f;

        [Header("Spell Logic")]
        [Required] [SerializeField] private List<AbilityRequirement> abilityRequirements = new List<AbilityRequirement>();
        [Required] [SerializeField] private List<AbilityAction> abilityActions = new List<AbilityAction>();

        public override string ColouredName
        {
            get
            {
                string hexColour = ColorUtility.ToHtmlStringRGB(ElementTree.TextColour);
                return $"<color=#{hexColour}>{Name}</color>";
            }
        }
        public string Description { get { return description; } }
        public ElementTree ElementTree { get { return elementTree; } }
        public float MaxCooldownDuration { get { return maxCooldownDuration; } }
        public float ChannelDuration { get { return channelDuration; } }
        public List<AbilityRequirement> AbilityRequirements { get { return abilityRequirements; } }
        public List<AbilityAction> AbilityActions { get { return abilityActions; } }

        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

            foreach (AbilityRequirement abilityRequirement in abilityRequirements)
            {
                builder.Append(abilityRequirement.GetDisplayText()).AppendLine();
            }
            builder.Append("Cast Time: ").Append(ChannelDuration).Append(" sec").AppendLine();
            builder.Append(Description);

            return builder.ToString();
        }

        public void Use() => onUseablePressed.Raise(this);
    }
}
