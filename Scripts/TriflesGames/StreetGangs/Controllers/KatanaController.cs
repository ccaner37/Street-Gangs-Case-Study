using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriflesGames.StreetGangs.Controllers
{
    public class KatanaController : MonoBehaviour
    {
        private Vector2 turn;
        private Vector3 deltaMove;

        private float sensitivity = 0.5f;
        private float speed = 2.0f;

        void Update()
        {
            KatanaControl();
        }

        private void KatanaControl()
        {
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
            turn.y += Input.GetAxis("Mouse Y") * sensitivity;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, turn.y * speed);
        }
    }
}