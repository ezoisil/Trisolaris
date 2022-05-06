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
    }
}
