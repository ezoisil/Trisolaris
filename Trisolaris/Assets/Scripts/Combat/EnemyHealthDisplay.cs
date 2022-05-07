using System;
using Trisolaris.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Trisolaris.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if(fighter.GetTarget() == null)
            {
                GetComponent<Text>().text = "No target";
                return;
            }
            GetComponent<Text>().text = String.Format("{0:0}%", fighter.GetTarget().GetPercentage().ToString());
        }
    }
}
