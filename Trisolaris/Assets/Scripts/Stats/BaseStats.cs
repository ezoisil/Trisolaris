using UnityEngine;

namespace Trisolaris.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1f, 100f)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

      
        public float GetStat(Stat stat)
        {
            return progression.GetStat(characterClass,stat , GetLevel());
        }

        public int GetLevel()
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

    }

    
}
