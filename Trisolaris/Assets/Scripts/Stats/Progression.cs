using UnityEngine;


namespace Trisolaris.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass = null;


        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass characterClass;
            [SerializeField] ProgressionHealth[] healthLevel;
        }

        [System.Serializable]
        class ProgressionHealth
        {
            [SerializeField] float health;
        }

    }
}