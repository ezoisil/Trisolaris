using System;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClass = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;
        public float GetStat(CharacterClass characterClass,Stat stat ,int level)
        {

            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];
            if (levels.Length < level)
            {
                Debug.Log("Stat level out of reach in progresion");
                return 0;
            }
            return levels[level-1];
           
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach(ProgressionCharacterClass progressionClass in progressionCharacterClass)
            {

                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach(ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }



        }

        public int GetLevels(CharacterClass characterClass, Stat stat)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
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