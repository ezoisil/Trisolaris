using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.Core
{
    public class CameraFacing : MonoBehaviour
    {
        // LateUpdate is used to resolve the race condition. Otherwise, parent update overrides this update and feature doesn't work as it should.
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }

    }
}
