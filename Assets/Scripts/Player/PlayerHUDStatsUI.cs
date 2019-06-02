using Hel.Combat;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hel.Player
{
    public class PlayerHUDStatsUI : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerStatsDataHolder playerStatsDataHolder;
        [Required] [SerializeField] private Slider healthbarSlider;
        [Required] [SerializeField] private TextMeshProUGUI healthbarSliderText;
        [Required] [SerializeField] private Slider manabarSlider;
        [Required] [SerializeField] private TextMeshProUGUI manabarSliderText;
        [Required] [SerializeField] private Image experienceImage;
        [Required] [SerializeField] private TextMeshProUGUI playerLevelText;

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
