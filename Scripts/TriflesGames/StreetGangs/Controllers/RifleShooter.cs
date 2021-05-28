using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TriflesGames.StreetGangs.Managers;

namespace TriflesGames.StreetGangs.Controllers
{
    public class RifleShooter : MonoBehaviour
    {
        [SerializeField]
        private Image crosshair;

        private float fireRate = 0.1f;
        private float rifleDamage = 3f;

        private void OnEnable()
        {
            crosshair.gameObject.SetActive(true);
            InvokeRepeating(nameof(Hit), 0, fireRate);
        }
        private void OnDisable()
        {
            crosshair.gameObject.SetActive(false);
            CancelInvoke();
        }

        void Hit()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0.13f, -0.03f, 1)), out hit, Mathf.Infinity))
            {
                CheckEnemy(hit);

                if (hit.collider.gameObject.GetComponent<PrefabManager>() == null)
                {
                    return;
                }

                DestroyObjects(hit);
            }
        }

        private void CheckEnemy(RaycastHit hit)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                crosshair.GetComponent<Image>().color = Color.red;
                hit.collider.gameObject.GetComponent<EnemyHealthController>().health = hit.collider.gameObject.GetComponent<EnemyHealthController>().health - rifleDamage;
                hit.collider.gameObject.GetComponent<EnemyParticleController>().HitEffect();
            }
            else
            {
                crosshair.GetComponent<Image>().color = Color.white;
            }
        }

        private void DestroyObjects(RaycastHit hit)
        {
            GameObject brokenPrefab = hit.collider.gameObject.GetComponent<PrefabManager>().brokenPrefab;

            if (brokenPrefab != null)
            {
                GameObject createdBroken = Instantiate(brokenPrefab, hit.collider.transform.position, hit.collider.transform.rotation);
                Destroy(hit.collider.gameObject);

                for (int i = 0; i < createdBroken.transform.childCount; i++)
                {
                    var child = createdBroken.transform.GetChild(i);
                    var childPos = createdBroken.transform.position;
                    child.GetComponent<Rigidbody>().AddExplosionForce(12, new Vector3(childPos.x + Random.Range(-2, 2), childPos.y + Random.Range(0, 5), childPos.z + Random.Range(-2, 2)), 100, 1, ForceMode.Impulse);
                }
            }
        }
    }
}