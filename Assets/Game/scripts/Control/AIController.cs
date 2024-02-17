using RPG.Combat;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspiciousTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0f, 1f)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        Fighter fighter;
        GameObject player;
        Health health;

        //used for guarding behaviour
        Vector3 guardPosition;
        Mover mover;

        //used for suspecious behaviour
        float timeSinceLastSawPlayer = Mathf.Infinity;


        int currentWaypointIndex = 0;
        float timeSinceArrivedAtWaypoint = 3f;


        // Start is called before the first frame update
        void Start()
        {

            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;

        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) { return; }
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspiciousTime)
            {
                //suspicious state
                SuspiciousBehaviour();
            }
            else
            {
                PatrolBehaviour();
                //fighter.Cancel();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if(patrolPath!= null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
                print(nextPosition);
            }
            if(timeSinceArrivedAtWaypoint>waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition,patrolSpeedFraction);
            }
            
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position,GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private void SuspiciousBehaviour()
        {
            GetComponent<ActionSchedular>().CancelCurrentAction();
            print("suspisious");
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //called by unity 
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
