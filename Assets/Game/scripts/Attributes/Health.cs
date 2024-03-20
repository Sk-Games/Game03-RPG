
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;

namespace RPG.Attribute
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;
        bool isDead = false;

        private void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
            
        }
        public void TakeDamage(GameObject instigator,float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                AwardExperience(instigator);
                Die();
                
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) { return; }
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100* (healthPoints/GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void Die()
        {
            if(isDead)
            {
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionSchedular>().CancelCurrentAction();
            
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if(healthPoints == 0)
            {
                Die();
            }
        }
    }
}
