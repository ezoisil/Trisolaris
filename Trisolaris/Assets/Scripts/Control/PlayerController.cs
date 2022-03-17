using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trisolaris.Movement;
using System;
using Trisolaris.Combat;

namespace Trisolaris.Control
{
    public class PlayerController : MonoBehaviour
    {
        int ATTACK_BUTTON = 1;
        int MOVE_BUTTON = 0;
        
        private void Update()
        {
            if(InteractWithCombat())return;
            if (InteractWithMovement()) return; 
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) 
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(ATTACK_BUTTON))
                {
                    GetComponent<Fighter>().Attack(target);                    
                }else if (Input.GetMouseButton(MOVE_BUTTON))
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            
            if (hasHit)
            {
                if(Input.GetMouseButton(MOVE_BUTTON) || Input.GetMouseButton(ATTACK_BUTTON))
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
