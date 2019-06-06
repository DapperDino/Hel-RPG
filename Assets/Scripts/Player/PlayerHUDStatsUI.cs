using Hel.Combat;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Player
{
    /// <summary>
    /// Used to display the player's stats to the HUD.
    /// </summary>
    public class PlayerHUDStatsUI : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerStatsDataHolder playerStatsDataHolder = null;
        [Required] [SerializeField] private Slider healthbarSlider = null;
        [Required] [SerializeField] private TextMeshProUGUI healthbarSliderText = null;
        [Required] [SerializeField] private Slider manabarSlider = null;
        [Required] [SerializeField] private TextMeshProUGUI manabarSliderText = null;
        [Required] [SerializeField] private Image experienceImage = null;
        [Required] [SerializeField] private TextMeshProUGUI playerLevelText = null;

        private void Start()
        {
            UpdateHealthUI();
            UpdateManaUI();
            UpdateExperience();
        }

        public void UpdateHealthUI()
        {
            int currentHealth = playerStatsDataHolder.StatsHolder.Health;
            int maxHealth = playerStatsDataHolder.StatsHolder.GetStatValue(StatTypes.MaxHealth);
            healthbarSlider.value = (float)currentHealth / maxHealth;
            healthbarSliderText.text = $"{currentHealth}/{maxHealth}";
        }

        public void UpdateManaUI()
        {
            int currentMana = (int)playerStatsDataHolder.Mana;
            int maxMana = playerStatsDataHolder.StatsHolder.GetStatValue(StatTypes.MaxMana);
            manabarSlider.value = (float)currentMana / maxMana;
            manabarSliderText.text = $"{currentMana}/{maxMana}";
        }

        public void UpdateExperience()
        {
            playerLevelText.text = playerStatsDataHolder.LevelSystem.Level.ToString();
            experienceImage.fillAmount = (float)playerStatsDataHolder.LevelSystem.CurrentExperience / playerStatsDataHolder.LevelSystem.ExperienceForLevelUp;
        }
    }
}
