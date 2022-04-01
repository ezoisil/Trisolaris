using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        private void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
