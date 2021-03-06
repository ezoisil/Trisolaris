using Trisolaris.Attributes;
using Trisolaris.Control;
using UnityEngine;

namespace Trisolaris.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (!GetComponent<Fighter>().CanAttack(callingController.gameObject))
            {
                return false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }
          
            return true;
        }
    }
}
