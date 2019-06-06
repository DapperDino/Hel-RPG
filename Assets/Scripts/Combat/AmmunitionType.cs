using UnityEngine;

namespace Hel.Combat
{
    /// <summary>
    /// Used to store data about different types of ammunition.
    /// </summary>
    [CreateAssetMenu(fileName = "New Ammunition Type", menuName = "Combat/Ammunition Type")]
    public class AmmunitionType : ScriptableObject
    {
        [SerializeField] private new string name = "New Ammunition Type Name";
        [Multiline] [SerializeField] private string description = "New Ammunition Type Description";

        public string Name { get { return name; } }
        public string Description { get { return description; } }
    }
}
