using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        private void Update()
        {           
            DistanceToPlayer();

            if (DistanceToPlayer() < chaseDistance)
            {
                print(gameObject.name + ": Stop where you are!");
            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}  
