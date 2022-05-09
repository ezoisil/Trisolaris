using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trisolaris.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        Text levelText;
        BaseStats baseStats;

        private void Awake()
        {
            levelText = GetComponent<Text>();
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            levelText.text = baseStats.GetLevel().ToString();
        }
    }
}
