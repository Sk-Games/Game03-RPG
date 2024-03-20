using RPG.Attribute;
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

        int currentLevel = 0;

        private void Update()
        {
            if(gameObject.tag == "Player")
            {
                print(GetLevel());
            }
        }
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();

            if(experience == null) { return startinglevel; }

            float currentXP = experience.GetPoints();

            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUP = progression.GetStat(Stat.ExperienceToLevelUp, characterClass,level);
                if (XPToLevelUP > currentXP) 
                { 
                    return level; 
                }
            }
            return penultimateLevel+1;
        }

        
    }
}
