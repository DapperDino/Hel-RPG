using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Items.Currencies
{
    /// <summary>
    /// Stores and handles all of an entity's currencies.
    /// </summary>
    [Serializable]
    public class CurrencyHolder
    {
        [SerializeField] private List<CurrencyData> currencyData = new List<CurrencyData>();

        public Action OnCurrencyValuesChanged = delegate { };

        public void ChangeCurrencyAmount(CurrencyTypes currencyType, int amount)
        {
            for (int i = 0; i < currencyData.Count; i++)
            {
                if (currencyData[i].Currency.CurrencyType == currencyType)
                {
                    currencyData[i].Amount += amount;
                    OnCurrencyValuesChanged.Invoke();
                    return;
                }
            }
        }
        public void ChangeCurrencyAmount(Currency currency, int amount)
        {
            for (int i = 0; i < currencyData.Count; i++)
            {
                if (currencyData[i].Currency == currency)
                {
                    currencyData[i].Amount += amount;
                    OnCurrencyValuesChanged.Invoke();
                    return;
                }
            }
        }

        public CurrencyData GetCurrencyData(CurrencyTypes currencyType)
        {
            foreach (CurrencyData data in currencyData)
            {
                if (data.Currency.CurrencyType == currencyType)
                {
                    return data;
                }
            }
            return null;
        }
        public CurrencyData GetCurrencyData(Currency currency)
        {
            foreach (CurrencyData data in currencyData)
            {
                if (data.Currency == currency)
                {
                    return data;
                }
            }
            return null;
        }

        public int GetCurrencyAmount(CurrencyTypes currencyType)
        {
            foreach (CurrencyData data in currencyData)
            {
                if (data.Currency.CurrencyType == currencyType)
                {
                    return data.Amount;
                }
            }
            return 0;
        }
        public int GetCurrencyAmount(Currency currency)
        {
            foreach (CurrencyData data in currencyData)
            {
                if (data.Currency == currency)
                {
                    return data.Amount;
                }
            }
            return 0;
        }
    }
}
