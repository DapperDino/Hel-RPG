using UnityEngine;

namespace Hel.Items.Currencies
{
    /// <summary>
    /// Used to store data about different types of currencies.
    /// </summary>
    [CreateAssetMenu(fileName = "New Currency", menuName = "Items/Currencies/Currency")]
    public class Currency : ScriptableObject
    {
        [SerializeField] private CurrencyTypes currencyType = CurrencyTypes.None;
        [SerializeField] private new string name = "New Currency Name";
        [SerializeField] private string description = "New Currency Description";

        public CurrencyTypes CurrencyType { get { return currencyType; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
    }
}

