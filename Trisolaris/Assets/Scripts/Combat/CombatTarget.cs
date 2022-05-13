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

            if (Input.GetMouseButton(1))
            {
                GetComponent<Fighter>().Attack(callingController.gameObject);
            }
          
            return true;
        }
    }
}
