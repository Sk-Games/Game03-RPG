using RPG.Attribute;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startinglevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;

        public event Action onLevelUp;


        int currentLevel = 0;
        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null)
            {
                //print("experience gained");
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()//edit public to private
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                //print("leveled Up!");
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();

            }
        }

        private void LevelUpEffect()
        {
            //print("level up particle system");
            Instantiate(levelUpParticleEffect,transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, CalculateLevel());
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {

                currentLevel = CalculateLevel();
            }
            print("current level"+currentLevel);
            return currentLevel;
        }
        
        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            //print("experience component gained "+ experience.name);

            if(experience == null) { return startinglevel; }

            float currentXP = experience.GetPoints();

            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            print("solution01: "+penultimateLevel);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUP = progression.GetStat(Stat.ExperienceToLevelUp, characterClass,level);
                if (XPToLevelUP > currentXP) 
                {
                    //print(level);
                    return level; 
                }
            }
            return penultimateLevel+1;
        }

        
    }
}
