using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Combat
{
    /// <summary>
    /// Stores and handles all of an entity's stats.
    /// </summary>

    [Serializable]
    public class StatsHolder
    {
        [SerializeField] private int health = 0;
        [Required] [SerializeField] private List<StatData> statData = new List<StatData>();

        public Action OnHealthChanged = delegate { };
        public Action OnDied = delegate { };

        public int Health { get { return health; } private set { health = value; } }

        [Button]
        public void ResetHealth()
        {
            Health = GetStatValue(StatTypes.MaxHealth);
            OnHealthChanged.Invoke();
        }

        public void DealDamage(DamageData damageData)
        {
            //Subtract the damage being dealt from our health.
            Health -= damageData.rawDamage;

            //Make sure that our health stays in the range of 0 to max health.
            Health = Mathf.Clamp(Health, 0, GetStatValue(StatTypes.MaxHealth));

            //Alert any listeners that we have died.
            if (Health == 0) { OnDied.Invoke(); }

            //Alert any listeners that our health has been changed.
            OnHealthChanged.Invoke();
        }

        public void Heal(int healAmount)
        {
            //Add the heal amount to our health.
            Health += healAmount;

            //Make sure that our health stays in the range of 0 to max health.
            Health = Mathf.Clamp(Health, 0, GetStatValue(StatTypes.MaxHealth));

            //Alert any listeners that our health has been changed.
            OnHealthChanged.Invoke();
        }

        public void SetAllDirty()
        {
            //Set all of our stats to dirty (to be recalculated).
            foreach (StatData data in statData)
            {
                data.SetDirty();
            }
        }

        public StatData GetStatData(StatTypes statType)
        {
            foreach (StatData data in statData)
            {
                if (data.Stat.StatType == statType)
                {
                    return data;
                }
            }
            return null;
        }
        public StatData GetStatData(Stat stat)
        {
            foreach (StatData data in statData)
            {
                if (data.Stat == stat)
                {
                    return data;
                }
            }
            return null;
        }

        public int GetStatValue(StatTypes statType)
        {
            foreach (StatData data in statData)
            {
                if (data.Stat.StatType == statType)
                {
                    return data.Value;
                }
            }
            return 0;
        }
        public int GetStatValue(Stat stat)
        {
            foreach (StatData data in statData)
            {
                if (data.Stat == stat)
                {
                    return data.Value;
                }
            }
            return 0;
        }
    }
}
