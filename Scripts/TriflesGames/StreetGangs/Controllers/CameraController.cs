using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriflesGames.StreetGangs.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public Vector2 turn;

        private Vector3 firstCamera;

            // = new Vector3(24.41f, 9.038f, 0);

        private Quaternion _targetRotation = Quaternion.identity;

        private float sensitivity = 0.5f;
        public float shakeAmount = 0.20f;
        private float decreaseFactor = 1.0f;
        public float shakeDuration = 0f;

        Vector3 originalPos;

        private WeaponController _weaponController;

        private void OnEnable()
        {
            originalPos = transform.localPosition;
        }

        private void Start()
        {
            _weaponController = FindObjectOfType<WeaponController>();
            firstCamera = transform.rotation.eulerAngles;
            _targetRotation = Quaternion.Euler(firstCamera);
        }

        private void Update()
        {
            ControlRifle();

            ShakeScreen();
        }

        private void ShakeScreen()
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime * 4);

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = originalPos;
            }
        }

        private void ControlRifle()
        {
            if (_weaponController.isUsingRifle)
            {
                turn.x += Input.GetAxis("Mouse X") * sensitivity;
                turn.y += Input.GetAxis("Mouse Y") * sensitivity;
                Quaternion a = Quaternion.Euler(-turn.y, turn.x, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, a, 70 * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 18 * Time.deltaTime);
            }
        }
    }
}
