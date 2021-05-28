using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriflesGames.StreetGangs.Controllers
{
    public class EnemyAnimationController : MonoBehaviour
    {
        private EnemyNavMeshController enemyNav;

        private Animator animator;

        public enum enemyState
        {
            shooting,
            walking,
            hiding,
            dead,
        }

        private enemyState currentState;

        private void Awake()
        {
            enemyNav = gameObject.GetComponent<EnemyNavMeshController>();
            animator = gameObject.GetComponent<Animator>();
        }

        public void ChangeAnimation(enemyState state)
        {
            currentState = state;
            CheckAnimation();
        }

        private void CheckAnimation()
        {
            switch (currentState)
            {
                case enemyState.shooting:
                    animator.SetBool("isEnemyShooting", true);
                    animator.SetBool("isEnemyIdle", false);
                    break;
                case enemyState.walking:
                    break;
                case enemyState.hiding:
                    animator.SetBool("isEnemyShooting", false);
                    animator.SetBool("isEnemyIdle", true);
                    enemyNav.SetDestination();
                    break;
                case enemyState.dead:
                    animator.SetBool("isEnemyDead", true);
                    break;
                default:
                    break;
            }
        }
    }
}
