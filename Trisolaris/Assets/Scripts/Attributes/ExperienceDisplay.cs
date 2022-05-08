using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trisolaris.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience playerExperience = null;

        private void Awake()
        {
            playerExperience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}",playerExperience.GetExperiencePoints());
        }
    }
}
