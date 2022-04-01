using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Trisolaris.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && !isTriggered)
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
           
        }
    }
}
