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
                Vector3 childPosition= transform.GetChild(i).transform.position;
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(childPosition, .2f);
            } 
        }
    }
}
