using Hel.Combat;
using Hel.Events.CustomEvents;
using Hel.Items;
using Hel.SavingLoading;
using Hel.Utilities;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hel.Player
{
    [CreateAssetMenu(fileName = "New Player Stats Holder", menuName = "Player/Stats Holder")]
    public class PlayerStatsHolder : SerializedScriptableObject, ITickable, ISaveable
    {
        [Header("Events")]
        [Required] [SerializeField] private VoidEvent onPlayerHealthChanged;
        [Required] [SerializeField] private VoidEvent onPlayerManaChanged;
        [Required] [SerializeField] private VoidEvent onPlayerDied;
        [Required] [SerializeField] private VoidEvent onPlayerExperienceChanged;
        [Required] [SerializeField] private VoidEvent onPlayerLevelUp;
        [Required] [SerializeField] private VoidEvent onPlayerCurrenciesChanged;

        [Header("Data Holders")]
        [Required] [SerializeField] private StatsHolder defaultStats;
        [Required] [SerializeField] private CurrencyHolder defaultCurrencies;

        [Header("On Level Up")]
        [Required] [SerializeField] private Dictionary<Stat, StatModifier> levelUpStatChanges = new Dictionary<Stat, StatModifier>();
        [Required] [SerializeField] private Dictionary<Currency, int> levelUpCurrencyChanges = new Dictionary<Currency, int>();

        private float mana;

        public StatsHolder StatsHolder { get; private set; } = new StatsHolder();
        public LevelSystem LevelSystem { get; } = new LevelSystem();
        public CurrencyHolder CurrencyHolder { get; } = new CurrencyHolder();
        public float Mana
        {
            get => mana;
            set
            {
                mana = value;
                mana = Mathf.Clamp(mana, 0f, StatsHolder.GetStatValue(StatTypes.MaxMana));
                onPlayerManaChanged.Raise();
            }
        }
        public int LoadPriority { get { return 100; } }

        private void OnEnable()
        {
            StatsHolder.OnHealthChanged += onPlayerHealthChanged.Raise;
            StatsHolder.OnDied += onPlayerDied.Raise;
            LevelSystem.OnExperienceGained += onPlayerExperienceChanged.Raise;
            LevelSystem.OnLevelUp += onPlayerLevelUp.Raise;
            CurrencyHolder.OnCurrencyValuesChanged += onPlayerCurrenciesChanged.Raise;
        }

        private void OnDisable()
        {
            StatsHolder.OnHealthChanged -= onPlayerHealthChanged.Raise;
            StatsHolder.OnDied -= onPlayerDied.Raise;
            LevelSystem.OnExperienceGained -= onPlayerExperienceChanged.Raise;
            LevelSystem.OnLevelUp -= onPlayerLevelUp.Raise;
            CurrencyHolder.OnCurrencyValuesChanged -= onPlayerCurrenciesChanged.Raise;
        }

        #region Mana

        public void Tick()
        {
            RegenerateMana();
        }

        [Button]
        public void ResetMana()
        {
            Mana = StatsHolder.GetStatValue(StatTypes.MaxMana);
            onPlayerManaChanged.Raise();
        }

        private void RegenerateMana()
        {
            Mana += Time.deltaTime * StatsHolder.GetStatValue(StatTypes.ManaRegen);
        }

        #endregion

        #region Level Up

        private void GiveLevelUpRewards()
        {
            foreach (KeyValuePair<Stat, StatModifier> statChange in levelUpStatChanges)
            {
                StatsHolder.GetStatData(statChange.Key).AddModifier(statChange.Value);
            }
            StatsHolder.ResetHealth();
            ResetMana();
            foreach (KeyValuePair<Currency, int> currencyChange in levelUpCurrencyChanges)
            {
                CurrencyHolder.ChangeCurrencyAmount(currencyChange.Key, currencyChange.Value);
            }
        }

        #endregion

        public void Save()
        {
            GameSaveHandler.SaveFile("player_stats", StatsHolder);
            GameSaveHandler.SaveFile("player_levelSystem", LevelSystem);
            GameSaveHandler.SaveFile("player_currencies", CurrencyHolder);
        }

        public void Load()
        {
            //Load Level System
            GameSaveHandler.LoadFile("player_levelSystem", LevelSystem);
            onPlayerExperienceChanged.Raise();

            //Load Currencies
            if (!GameSaveHandler.LoadFile("player_currencies", CurrencyHolder))
            {
                JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(defaultCurrencies), CurrencyHolder);
            }
            onPlayerCurrenciesChanged.Raise();

            //Load Stats
            if (!GameSaveHandler.LoadFile("player_stats", StatsHolder))
            {
                JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(defaultStats), StatsHolder);
            }
            StatsHolder.SetAllDirty();
            onPlayerHealthChanged.Raise();
            onPlayerManaChanged.Raise();
            ResetMana();
        }
    }
}