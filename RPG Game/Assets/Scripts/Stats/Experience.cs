using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        //public delegate void ExperienceGainedDelegate();
        //public event ExperienceGainedDelegate onExperienceGained;
        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        public object CaputureState()
        {
            return experiencePoints;
        } 
        
        public float GetPoint()
        {
            return experiencePoints;
        }
    }
}

