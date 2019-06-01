using UnityEngine;

namespace Hel.Items
{
    [CreateAssetMenu(fileName = "New Rarity", menuName = "Items/Rarity")]
    public class Rarity : ScriptableObject
    {
        [SerializeField] private new string name = "New Rarity Name";
        [SerializeField] private Color textColour;

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
    }
}
