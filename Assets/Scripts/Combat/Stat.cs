using UnityEngine;

namespace Hel.Combat
{
    [CreateAssetMenu(fileName = "New Stat", menuName = "Combat/Stat")]
    public class Stat : ScriptableObject
    {
        [SerializeField] private StatTypes statType;
        [SerializeField] private new string name = "New Stat Name";
        [Multiline] [SerializeField] private string description = "New Stat Description";

        public StatTypes StatType { get { return statType; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
    }
}
