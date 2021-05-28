using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriflesGames.StreetGangs.Managers
{
    public class PlayerHealthManager : MonoBehaviour
    {
        private UIManager _uiManager;

        [SerializeField]
        private GameObject bloodEffect;

        private GameManager _gameManager;

        public int health = 100;

        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        public void CheckHealth()
        {
            if (health == 0)
            {
                _uiManager.PlayAgain();
            }
            else
            {
                bloodEffect.gameObject.SetActive(true);
            }
        }

    }
}