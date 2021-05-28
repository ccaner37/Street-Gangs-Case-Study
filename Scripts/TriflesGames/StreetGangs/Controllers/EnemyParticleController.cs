using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriflesGames.StreetGangs.Controllers
{
    public class EnemyParticleController : MonoBehaviour
    {
        private ParticleSystem muzzleFlash;

        private EnemyAnimationController _animationController;

        [SerializeField]
        private Material whiteEffect;
        private Material tempMaterial;

        private void Start()
        {
            _animationController = GameObject.FindObjectOfType<EnemyAnimationController>();
            muzzleFlash = gameObject.GetComponentInChildren<ParticleSystem>();
        }

        public void MuzzleFlashEffect()
        {
            muzzleFlash.Play();
        }

        public void HitEffect()
        {
            tempMaterial = gameObject.GetComponentInChildren<Renderer>().material;
            gameObject.GetComponentInChildren<Renderer>().material = whiteEffect;
            StartCoroutine(GiveMaterialBack());
        }

        IEnumerator GiveMaterialBack()
        {
            yield return new WaitForSeconds(0.015f);
            gameObject.GetComponentInChildren<Renderer>().material = tempMaterial;
        }
    }
}
