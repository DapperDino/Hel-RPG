using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Magic
{
    /// <summary>
    /// Used to store data about different element trees.
    /// </summary>
    [CreateAssetMenu(fileName = "New Element Tree", menuName = "Magic/Element Tree")]
    public class ElementTree : SerializedScriptableObject
    {
        [SerializeField] private new string name = "New Element Tree Name";
        [SerializeField] private Color textColour = new Color(1f, 1f, 1f, 1f);

        [Header("Spells")]
        [SerializeField] private Spell[,] treeSpells = new Spell[7, 3];

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
        public Spell[,] TreeSpells { get { return treeSpells; } }
    }
}
