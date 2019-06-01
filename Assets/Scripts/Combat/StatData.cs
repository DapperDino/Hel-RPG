using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Combat
{
    [Serializable]
    public class StatData
    {
        [Required] [SerializeField] private Stat stat;
        [SerializeField] private int baseValue;
        [Required] [SerializeField] private List<StatModifier> statModifiers = new List<StatModifier>();

        public Stat Stat { get { return stat; } }
        public int Value
        {
            get
            {
                if (isDirty || baseValue != lastBaseValue)
                {
                    lastBaseValue = baseValue;
                    value = CalculateFinalValue();
                    isDirty = false;
                }
                return value;
            }
        }

        private bool isDirty = true;
        private int value;
        private int lastBaseValue = int.MinValue;

        public void SetDirty() => isDirty = true;

        public void AddModifier(StatModifier statModifier)
        {
            statModifiers.Add(statModifier);
            statModifiers.Sort(CompareModifierOrder);
            isDirty = true;
        }

        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Type < b.Type) { return -1; }
            else if (a.Type > b.Type) { return 1; }
            return 0;
        }

        public bool RemoveModifier(StatModifier statModifier)
        {
            if (statModifiers.Remove(statModifier))
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        private int CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentageAdd = 0f;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                switch (statModifiers[i].Type)
                {
                    case StatModifierType.Flat:
                        finalValue += statModifiers[i].Value;
                        break;

                    case StatModifierType.PercentageAdditive:
                        sumPercentageAdd += statModifiers[i].Value;
                        if (i + 1 == statModifiers.Count)
                        {
                            finalValue *= 1 + sumPercentageAdd;
                            sumPercentageAdd = 0f;
                        }
                        break;
                }
            }

            return Mathf.RoundToInt(finalValue);
        }
    }
}
