using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriflesGames.StreetGangs.Controllers
{ 
    public class EnemyShootController : MonoBehaviour
    {
        private WeaponController _weaponController;

        [SerializeField]
        private GameObject bulletPrefab;
        private GameObject bullet;

        private EnemyParticleController _particleController;

        private Vector3 bulletPosition;

        private void Awake()
        {
            _particleController = GetComponentInChildren<EnemyParticleController>();
        }

        void Start()
        {
            bulletPosition = new Vector3(transform.position.x - 0.25f, transform.position.y + 1.15f, transform.position.z - 0.85f);
            InvokeRepeating(nameof(EnemyShoot), 0.1f, Random.Range(1, 3));
            _weaponController = FindObjectOfType<WeaponController>();
        }

        private void LateUpdate()
        {
            bulletPosition = new Vector3(transform.position.x - 0.25f, transform.position.y + 1.15f, transform.position.z - 0.85f);
        }

        void EnemyShoot()
        {
            if (_weaponController.isUsingKatana)
            {
                _particleController.MuzzleFlashEffect();
                transform.LookAt(GameObject.FindGameObjectWithTag("SlowMotionArea").transform);
                bullet = Instantiate(bulletPrefab, bulletPosition, bulletPrefab.transform.rotation);
            }
            else
            {
                foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("Bullet"))
                {
                    Destroy(tmp);
                }
            }
        }
    }
}