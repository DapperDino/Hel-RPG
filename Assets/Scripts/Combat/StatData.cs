using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Combat
{
    /// <summary>
    /// Used to store data about an instance of a stat.
    /// </summary>
    [Serializable]
    public class StatData
    {
        [Required] [SerializeField] private Stat stat = null;
        [SerializeField] private int baseValue = 0;
        [Required] [SerializeField] private List<StatModifier> statModifiers = new List<StatModifier>();

        private bool isDirty = true;
        private int value = 0;
        private int lastBaseValue = int.MinValue;

        public Stat Stat { get { return stat; } }
        public int Value
        {
            get
            {
                //Only recalculate the stat if it has been changed since the last tme it was requested.
                if (isDirty || baseValue != lastBaseValue)
                {
                    lastBaseValue = baseValue;
                    value = CalculateFinalValue();
                    isDirty = false;
                }
                return value;
            }
        }

        public void SetDirty() => isDirty = true;

        public void AddModifier(StatModifier statModifier)
        {
            //Add the modifier to the list of all current stat modifiers.
            statModifiers.Add(statModifier);

            //Sort into order by type.
            statModifiers.Sort(CompareModifierOrder);

            //Set dirty to recalculate "Value" on next request.
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
            //Make sure that the modifier is already applied before trying to remove.
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

            //Handle stat calculation based on the modifier type.
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
