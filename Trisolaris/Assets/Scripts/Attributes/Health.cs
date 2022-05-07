using UnityEngine;
using Trisolaris.Saving;
using Trisolaris.Stats;
using Trisolaris.Core;

namespace Trisolaris.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100;
        bool isDead = false;

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(float damage)
        {
            if (isDead) return;
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
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
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            
            if (healthPoints == 0)
            {
                Die();
                isDead = true;
            }
        }

        public float GetPercentage()
        {
            return (healthPoints / GetComponent<BaseStats>().GetHealth()) * 100;
        }
    }
}
