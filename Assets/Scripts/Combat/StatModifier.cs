using System;
using UnityEngine;

namespace Hel.Combat
{
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private float value;
        [SerializeField] private StatModifierType type;

        public float Value { get { return value; } }
        public StatModifierType Type { get { return type; } }

        public StatModifier(float value, StatModifierType type)
        {
            this.value = value;
            this.type = type;
        }
    }
}
