using System.Collections;
using Trisolaris.Control;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player") 
            {

            Pickup(other.gameObject.GetComponent<Fighter>());

            }


        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
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
                Pickup(callingController.GetComponent<Fighter>());
            }
            return true;
        }
    }
}
