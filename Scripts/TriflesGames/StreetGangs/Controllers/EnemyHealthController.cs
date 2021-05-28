using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TriflesGames.StreetGangs.Managers;

namespace TriflesGames.StreetGangs.Controllers
{
    public class EnemyHealthController : MonoBehaviour
    {
        private Rigidbody rb;

        bool isDead = true;

        private Image enemyHealthBar;

        private GameManager _gameManager;

        private EnemyAnimationController enemyAnimation;

        public float health = 100;
        private float speed = 2f;
        private float value;

        public int aliveEnemies;

        void Start()
        {
            _gameManager = GameManager.Instance;
            enemyAnimation = gameObject.GetComponent<EnemyAnimationController>();
            rb = gameObject.GetComponent<Rigidbody>();
            enemyHealthBar = transform.Find("Canvas/HealthBarBackground/EnemyHealthBar").gameObject.GetComponent<Image>();
            aliveEnemies = transform.parent.childCount;
        }

        private void Update()
        {
            UpdateHealthBar();

            CheckHealth();
        }

        private void UpdateHealthBar()
        {
            value = health / 100f;
            enemyHealthBar.fillAmount = Mathf.Lerp(enemyHealthBar.fillAmount, value, speed * Time.deltaTime);
        }

        private void CheckHealth()
        {
            if (health < 0 && isDead)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.AddExplosionForce(50, new Vector3(transform.position.x + 2, transform.position.y + 30, transform.position.z - 30), 100, 1, ForceMode.Impulse);
                Destroy(gameObject.GetComponent<EnemyShootController>());
                _gameManager.IsEveryoneDead();
                enemyAnimation.ChangeAnimation(EnemyAnimationController.enemyState.dead);
                isDead = false;
            }
        }
    }
}