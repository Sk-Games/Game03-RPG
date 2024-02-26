using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName ="Weapons/Make New Weapon", order =0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            Console.WriteLine("120");

            if (equippedPrefab!= null)
            {
                Console.WriteLine("420");
                Instantiate(equippedPrefab, handTransform);
            }
            
            if(animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            
        }

        

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}
