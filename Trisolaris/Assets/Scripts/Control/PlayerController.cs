using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trisolaris.Movement;

namespace Trisolaris.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }
        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }
}
