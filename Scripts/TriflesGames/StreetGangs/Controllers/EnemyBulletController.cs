using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriflesGames.StreetGangs.Managers;

namespace TriflesGames.StreetGangs.Controllers
{
    public class EnemyBulletController : MonoBehaviour
    {
        private CameraController _cameraController;

        private Vector3 endMarker;

        [SerializeField]
        private Transform bulletWay;

        private GameObject bullet;
        [SerializeField]
        private GameObject brokenBulletPrefab;

        private float speed = 0.70F;
        private float startTime;
        private float journeyLength;

        private PlayerHealthManager _playerHealth;

        private void Start()
        {
            CreateBullet();
            _cameraController = FindObjectOfType<CameraController>();
            _playerHealth = FindObjectOfType<PlayerHealthManager>();
        }

        private void CreateBullet()
        {
            bulletWay = GameObject.FindGameObjectWithTag("BulletWay").transform;
            endMarker = bulletWay.position;
            endMarker = new Vector3(endMarker.x + Random.Range(-0.15f, 0.10f), endMarker.y + Random.Range(-0.10f, 0.30f), endMarker.z);
            transform.LookAt(bulletWay);
            transform.eulerAngles = new Vector3(155, transform.eulerAngles.y, transform.eulerAngles.z);
            startTime = Time.time;
            journeyLength = Vector3.Distance(transform.position, endMarker);
        }

        private void FixedUpdate()
        {
            MoveBullet();
        }

        private void MoveBullet()
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, endMarker, fractionOfJourney);
        }

        private void SlowMotionBullet()
        {
               speed = 0.035f;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "PlayerArea")
            {
                DamagePlayer();
            }
            if (collision.gameObject.tag == "SlowMotionArea")
            {
                SlowMotionBullet();
            }
        }

        private void DamagePlayer()
        {
            Destroy(transform.gameObject);
            _playerHealth.health -= 10;
            _playerHealth.CheckHealth();
            _cameraController.shakeDuration = 0.8f;
        }

        public void SliceBullet()
        {
            GameObject brokenBullet = Instantiate(brokenBulletPrefab, transform.position, brokenBulletPrefab.transform.rotation);
            for (int i = 0; i < brokenBullet.transform.childCount; i++)
            {
                Vector3 childTransform = brokenBullet.transform.GetChild(i).position;
                Rigidbody rigidbody = brokenBullet.transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
                if (i == 0)
                {
                    rigidbody.AddExplosionForce(1, new Vector3(childTransform.x + 0.23f, childTransform.y, childTransform.z), 100, 1, ForceMode.Impulse);
                }
                else
                {
                    rigidbody.AddExplosionForce(1, new Vector3(childTransform.x - 0.23f, childTransform.y, childTransform.z), 100, 1, ForceMode.Impulse);
                }
            }

            Destroy(brokenBullet, 1);
            Destroy(gameObject);
        }
    }
}