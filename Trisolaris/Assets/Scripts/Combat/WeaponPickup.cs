using System.Collections;
using Trisolaris.Attributes;
using Trisolaris.Control;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null;
        [SerializeField] float healthToRestore = 0;
        [SerializeField] float respawnTime = 5;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player") 
            {

            Pickup(other.gameObject);

            }


        }

        private void Pickup(GameObject subject)
        {
            if(weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon);
            }
            if(healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
            StartCoroutine(HideForSeconds(respawnTime));


        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }

            GetComponent<Collider>().enabled = shouldShow;
        
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}
