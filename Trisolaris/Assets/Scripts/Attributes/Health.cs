using UnityEngine;
using Trisolaris.Saving;
using Trisolaris.Stats;
using Trisolaris.Core;

namespace Trisolaris.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        
        float healthPoints = -1f;
        bool isDead = false;
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
        }

        private void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = baseStats.GetStat(Stat.Health);
            }
        }

        private void OnEnable()
        {
            baseStats.onLevelUp += RegenerateHealth;
        }
        private void OnDisable()
        {
            baseStats.onLevelUp -= RegenerateHealth;
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

        private void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * regenerationPercentage / 100;
            healthPoints = Mathf.Max(regenHealthPoints, healthPoints);
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

            experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));
            

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
            return (healthPoints / baseStats.GetStat(Stat.Health)) * 100;
        }
    }
}
