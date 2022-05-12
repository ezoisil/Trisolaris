using UnityEngine;
using Trisolaris.Movement;
using Trisolaris.Combat;
using Trisolaris.Attributes;

namespace Trisolaris.Control
{
    public class PlayerController : MonoBehaviour
    {
        int ATTACK_BUTTON = 1;
        int MOVE_BUTTON = 0;
        Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if(InteractWithCombat())return;
            if (InteractWithMovement()) return; 
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) 
                {
                    continue;
                }

                if (Input.GetMouseButton(ATTACK_BUTTON))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }else if (Input.GetMouseButton(MOVE_BUTTON))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
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
                    GetComponent<Mover>().StartMoveAction(hit.point,1f);
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
