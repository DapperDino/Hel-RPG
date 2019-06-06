using Hel.Items.Hotbars;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Items.Inventories
{
    /// <summary>
    /// The base class for anything that is able to go in an inventory.
    /// </summary>
    public abstract class InventoryItem : HotbarItem
    {
        [Header("Item Data")]
        [Required] [SerializeField] private Rarity rarity = null;
        [MinValue(0)] [SerializeField] private int sellPrice = 1;
        [MinValue(0)] [SerializeField] private int maxStack = 1;

        public override string ColouredName
        {
            get
            {
                string hexColour = ColorUtility.ToHtmlStringRGB(rarity.TextColour);
                return $"<color=#{hexColour}>{Name}</color>";
            }
        }
        public int SellPrice { get { return sellPrice; } }
        public int MaxStack { get { return maxStack; } }
        public Rarity Rarity { get { return rarity; } }
    }
}
