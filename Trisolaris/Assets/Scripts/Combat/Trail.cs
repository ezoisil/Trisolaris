using System.Collections;
using UnityEngine;

namespace Trisolaris.Combat
{

    public class Trail : MonoBehaviour
    {
        private TrailRenderer trailRenderer;
        private void Awake()
        {
            trailRenderer = GetComponent<TrailRenderer>();
            trailRenderer.enabled = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(StartTrail());
        }
        IEnumerator StartTrail()
        {
            yield return new WaitForSeconds(.001f);
            trailRenderer.enabled = true;

        }
       
    }
}
