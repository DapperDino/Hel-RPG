using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Combat
{

    [Serializable]
    public class StatsHolder
    {
        [SerializeField] private int health;
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
            Health -= damageData.rawDamage;
            Health = Mathf.Clamp(Health, 0, GetStatValue(StatTypes.MaxHealth));
            if (Health == 0) { OnDied.Invoke(); }
            OnHealthChanged.Invoke();
        }

        public void Heal(int healAmount)
        {
            Health += healAmount;
            Health = Mathf.Clamp(Health, 0, GetStatValue(StatTypes.MaxHealth));
            OnHealthChanged.Invoke();
        }

        public void SetAllDirty()
        {
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
