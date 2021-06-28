using RPG.Combat;
using RPG.Core;
using RPG.Movment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwllTime = 1f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        Fighter fighter;
        GameObject player;
        Mover mover;
        Health health;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtPoint = Mathf.Infinity;
        Vector3 guardPostion;
        int currentWaypointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();

            guardPostion = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;
            if (InattackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpadteTimers();
        }

        private void UpadteTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtPoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPostion;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtPoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtPoint > waypointDwllTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWayPoint(currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool InattackRangeOfPlayer()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

