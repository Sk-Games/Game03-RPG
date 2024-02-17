
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.core;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        //[SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;
        NavMeshAgent agent;
        Health health;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            agent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 globalVal = agent.velocity;
            Vector3 localVal = transform.InverseTransformDirection(globalVal);
            float speed = localVal.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            
            MoveTo(destination,speedFraction);

        }


        public void MoveTo(Vector3 destination, float speedFraction)
        {
            agent.destination = destination;
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            agent.isStopped = false;
            
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }
        
    }

}