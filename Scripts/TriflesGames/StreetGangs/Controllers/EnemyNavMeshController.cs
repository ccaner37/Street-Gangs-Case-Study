using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace TriflesGames
{
    public class EnemyNavMeshController : MonoBehaviour
    {
        private Animator animator;

        private NavMeshAgent navMeshAgent;

        [SerializeField]
        private Vector3 enemyDestination = new Vector3(0, 0, 0);

        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void LateUpdate()
        {
            CheckSpeed();
        }

        private void CheckSpeed()
        {
            float speed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("speed", speed);
        }

        public void SetDestination()
        {
            navMeshAgent.SetDestination(enemyDestination);
        }
    }
}
