using System.Collections;
using System.Collections.Generic;
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
            return progression.GetStat(characterClass,stat ,startingLevel);
        }

   

    }

    
}
