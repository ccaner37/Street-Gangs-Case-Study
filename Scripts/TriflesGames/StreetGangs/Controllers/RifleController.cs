using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TriflesGames.StreetGangs.Controllers
{
    public class RifleController : MonoBehaviour
    {
        [SerializeField]
        private Image ammo;
        [SerializeField]
        private Image ammoBackground;

        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip audioClip;

        private CameraController _cameraController;

        private float ammoRate = 0.002f;

        private void Awake()
        {
            _cameraController = FindObjectOfType<CameraController>();
        }

        private void OnEnable()
        {
            EnableRifle();
        }
        private void OnDisable()
        {
            DisableRifle();
        }

        private void EnableRifle()
        {
            ammoBackground.gameObject.SetActive(true);
            audioSource.PlayOneShot(audioClip);
            _cameraController.shakeAmount = 1.0f;
            _cameraController.shakeDuration = 10.0f;
        }

        private void DisableRifle()
        {
            ammo.fillAmount = 1;
            ammoBackground.gameObject.SetActive(false);
            _cameraController.shakeAmount = 0.20f;
        }

        private void FixedUpdate()
        {
            ammo.fillAmount -= ammoRate;
        }
    }
}
