using UnityEngine;
using UnityEngine.Playables;
using Trisolaris.Core;
using Trisolaris.Control;

namespace Trisolaris.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
        }
        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
