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
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            if (isDead) return;
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }

        }

       
       
        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            isDead = true;

        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;  
            
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
            

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
        public bool IsDead()
        {
            return isDead;
        }

        public float GetPercentage()
        {
            return (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100;
        }
    }
}
