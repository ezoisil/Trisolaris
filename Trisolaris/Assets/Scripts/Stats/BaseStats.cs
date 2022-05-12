using System;
using UnityEngine;

namespace Trisolaris.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1f, 100f)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifier = false;

        public event Action onLevelUp;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = GetLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifiers(stat)) * (1 + GetPercentageModifier(stat)/100);
        }

        

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(characterClass, stat, GetLevel());
        }

        public int GetLevel()
        {
            if(currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }
        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.GetExperiencePoints();
            int PenultimateLevel = progression.GetLevels(characterClass, Stat.ExperienceToLevelUp);
            for(int level = 1; level < PenultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(characterClass, Stat.ExperienceToLevelUp, level);
                if ( XPToLevelUp> currentXP)
                {
                    return level;
                }
            }

            return PenultimateLevel + 1;
        }
        private float GetAdditiveModifiers(Stat stat)
        {
            if(!shouldUseModifier) return 0;
            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetPercentageModifiers(stat))
                {
                    total *= modifier;
                }
            }
            return total;
        }


    }


}
