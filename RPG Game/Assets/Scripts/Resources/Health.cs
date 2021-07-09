using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        float healthPoint = -1f;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        private void Start()
        {
            if (healthPoint < 0)
            {
                healthPoint = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().OnLevelup += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().OnLevelup -= RegenerateHealth;
        }

        private void RegenerateHealth()
        {
            float regenerationPoint = GetComponent<BaseStats>().GetStat(Stat.Health) * regenerationPercentage / 100;
            healthPoint = Mathf.Max(healthPoint, regenerationPoint);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + "take" + damage);

            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if (healthPoint == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealth()
        {
            return healthPoint;
        }

        public float GetMaxHealthPoint()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoint / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public object CaptureState()
        {
            return healthPoint;
        }

        public void RestoreState(object state)
        {
            healthPoint = (float)state;
            if (healthPoint == 0)
            {
                Die();
            }
        }

        public object CaputureState()
        {
            return healthPoint;
        }
    }
}