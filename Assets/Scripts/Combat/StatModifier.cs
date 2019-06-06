using System;
using UnityEngine;

namespace Hel.Combat
{
    /// <summary>
    /// Stores the data about an instance of a stat modifier.
    /// </summary>
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private float value = 0f;
        [SerializeField] private StatModifierType type = StatModifierType.None;

        public float Value { get { return value; } }
        public StatModifierType Type { get { return type; } }

        public StatModifier(float value, StatModifierType type)
        {
            this.value = value;
            this.type = type;
        }
    }
}
