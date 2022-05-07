using UnityEngine;


namespace Trisolaris.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClass = null;

        public float GetStat(CharacterClass characterClass,Stat stat ,int level)
        {
            foreach(ProgressionCharacterClass character in progressionCharacterClass)
            {
                if(character.characterClass == characterClass)
                {
                    foreach(ProgressionStat progressionStat in character.stats)
                    {
                        if (stat == progressionStat.stat)
                        {
                            return progressionStat.levels[level - 1];
                        }
                    }
                }
            }
            Debug.LogError("no character match");
            return 0;
        }


        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public float[] levels;
            public Stat stat;
        }

    }
}