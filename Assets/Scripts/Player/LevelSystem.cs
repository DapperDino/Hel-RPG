using System;
using UnityEngine;

namespace Hel.Player
{
    [Serializable]
    public class LevelSystem
    {
        [SerializeField] private int level = 1;
        [SerializeField] private int currentExperience = 0;

        private const int BaseCapExperience = 100;
        private const float ExperienceCapMultiplier = 1.25f;

        public Action OnLevelUp = delegate { };
        public Action OnExperienceGained = delegate { };

        public int Level { get { return level; } }
        public int CurrentExperience { get { return currentExperience; } }
        public int ExperienceForLevelUp { get { return Mathf.RoundToInt(BaseCapExperience * Mathf.Pow(ExperienceCapMultiplier, Level)); } }

        public void AddExperience(int experienceToAdd)
        {
            currentExperience += experienceToAdd;
            OnExperienceGained.Invoke();

            while (currentExperience >= ExperienceForLevelUp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            currentExperience -= ExperienceForLevelUp;
            level++;
            OnLevelUp.Invoke();
        }
    }
}