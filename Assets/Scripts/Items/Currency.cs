using UnityEngine;

namespace Hel.Items
{
    [CreateAssetMenu]
    public class Currency : ScriptableObject
    {
        [SerializeField] private CurrencyTypes currencyType;
        [SerializeField] private new string name;
        [SerializeField] private string description;

        public CurrencyTypes CurrencyType { get { return currencyType; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
    }
}

