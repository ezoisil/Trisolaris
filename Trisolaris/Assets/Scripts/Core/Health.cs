using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trisolaris.Saving;

namespace Trisolaris.Core
{
    public class Health : MonoBehaviour, ISaveable
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
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            
            if (health == 0)
            {
                Die();
                isDead = true;
            }
        }
    }
}
