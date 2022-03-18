using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100;
        bool isDead = false;

        public void TakeDamage(float damage)
        {
            if (isDead) return;
            health = Mathf.Max(health - damage, 0);
            if(health == 0)
            {
                Die();
                isDead = true;
            }

        }

        public bool IsDead()
        {
            return isDead;
        }
        void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
