using UnityEngine;

namespace Hel.Combat
{
    [CreateAssetMenu(fileName = "New Damage Type", menuName = "Combat/Damage Type")]
    public class DamageType : ScriptableObject
    {
        [SerializeField] private new string name = "New Damage Type Name";
        [SerializeField] private Color textColour;

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
    }
}
