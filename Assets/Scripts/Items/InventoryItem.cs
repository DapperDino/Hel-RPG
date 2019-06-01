using Hel.Items.Hotbars;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Items
{
    public abstract class InventoryItem : HotbarItem
    {
        [Header("Item Data")]
        [Required] [SerializeField] private Rarity rarity;
        [SerializeField] private int sellPrice = 1;
        [SerializeField] private int maxStack = 1;

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
