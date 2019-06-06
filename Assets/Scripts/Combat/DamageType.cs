using UnityEngine;

namespace Hel.Combat
{
    /// <summary>
    /// Used to store data about different types of damage.
    /// </summary>
    [CreateAssetMenu(fileName = "New Damage Type", menuName = "Combat/Damage Type")]
    public class DamageType : ScriptableObject
    {
        [SerializeField] private new string name = "New Damage Type Name";
        [SerializeField] private Color textColour = new Color(1f, 1f, 1f, 1f);

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
    }
}
