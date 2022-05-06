using UnityEngine;


namespace Trisolaris.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClass = null;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach(ProgressionCharacterClass character in progressionCharacterClass)
            {
                if(character.characterClass == characterClass)
                {
                    return character.health[level-1];
                }
            }
            return 0;
        }


        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }

       

    }
}