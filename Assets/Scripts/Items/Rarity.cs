using UnityEngine;

namespace Hel.Items
{
    /// <summary>
    /// Used to store data about different rarities.
    /// </summary>
    [CreateAssetMenu(fileName = "New Rarity", menuName = "Items/Rarity")]
    public class Rarity : ScriptableObject
    {
        [SerializeField] private new string name = "New Rarity Name";
        [SerializeField] private Color textColour = new Color(1f, 1f, 1f, 1f);

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
    }
}
