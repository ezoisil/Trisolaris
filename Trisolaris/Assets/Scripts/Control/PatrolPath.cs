using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
           for(int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(GetWaypoint(i), .2f);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        private int GetNextIndex(int i)
        {
            if (i +1 >= transform.childCount)
            {
                return 0;
            }
            else return i +1;
        }

        private Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).transform.position;
        }
    }
}
