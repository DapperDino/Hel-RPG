using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Hel.Items.Currencies
{
    /// <summary>
    /// Used to store data about an instance of a currency.
    /// </summary>
    [Serializable]
    public class CurrencyData
    {
        [Required] [SerializeField] private Currency currency = null;
        [SerializeField] private int amount = 0;

        public Currency Currency { get { return currency; } }
        public int Amount { get { return amount; } set { amount = value; } }
    }
}
