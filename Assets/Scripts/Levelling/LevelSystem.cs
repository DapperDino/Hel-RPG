using System;
using UnityEngine;

namespace Hel.Levelling
{
    /// <summary>
    /// Stores and handles all of an entity's level data.
    /// </summary>
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
            //Increase our experience.
            currentExperience += experienceToAdd;

            //Alert any listeners that experience has been gained.
            OnExperienceGained.Invoke();

            //Check whether we have enough experience to level up.
            while (currentExperience >= ExperienceForLevelUp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            //Reduce our current experience.
            currentExperience -= ExperienceForLevelUp;

            //Increase our level.
            level++;

            //Alert any listeners that we have levelled up.
            OnLevelUp.Invoke();
        }
    }
}