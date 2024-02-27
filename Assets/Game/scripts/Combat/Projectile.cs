using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        Health target = null;

        private void Update()
        {
            if (target == null) return;
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        

        public void SetTarget(Health target) 
        {
            this.target = target;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = GetComponent<CapsuleCollider>();
            if (targetCapsule == null) { return target.transform.position; }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;

        }
    }
}
