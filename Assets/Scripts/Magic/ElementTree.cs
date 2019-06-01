using Sirenix.OdinInspector;
using UnityEngine;

namespace Hel.Magic
{
    [CreateAssetMenu(fileName = "New Element Tree", menuName = "Magic/Element Tree")]
    public class ElementTree : SerializedScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Color textColour;

        [Header("Spells")]
        [SerializeField] private Spell[,] treeSpells = new Spell[7, 3];

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
        public Spell[,] TreeSpells { get { return treeSpells; } }
    }
}
