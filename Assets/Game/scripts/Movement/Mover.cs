using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        //[SerializeField] Transform target;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 globalVal = agent.velocity;
            Vector3 localVal = transform.InverseTransformDirection(globalVal);
            float speed = localVal.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);

        }


        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
            agent.isStopped = false;
            
        }

        public void Stop()
        {
            agent.isStopped = true;
        }
    }

}