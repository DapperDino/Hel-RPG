using Sirenix.OdinInspector;
using System;

namespace Hel.Items
{
    [Serializable]
    public class CurrencyData
    {
        [Required] public Currency currency;
        public int amount;
    }
}
