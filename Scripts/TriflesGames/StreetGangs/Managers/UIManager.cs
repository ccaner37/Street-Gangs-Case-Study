using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TriflesGames.StreetGangs.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Image fillBar;

        [SerializeField]
        private Button start;
        [SerializeField]
        private Button restart;
        [SerializeField]
        private Button nextLevel;

        [SerializeField]
        private Text levelText;

        private GameManager _gameManager;

        private PlayerHealthManager _playerHealth;

        private AudioSource[] allAudioSources;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _playerHealth = FindObjectOfType<PlayerHealthManager>();
            SetLevelText();
        }

        private void Update()
        {
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            float value = _playerHealth.health / 100f;
            fillBar.fillAmount = Mathf.Lerp(fillBar.fillAmount, value, Time.deltaTime);
        }

        public void PlayAgain()
        {
            restart.gameObject.SetActive(true);
            fillBar.fillAmount = 0;
            StopAllAudio();
            _gameManager.PauseGame();
        }

        public void LevelCompleted()
        {
            restart.gameObject.SetActive(true);
            nextLevel.gameObject.SetActive(true);
            StopAllAudio();
            _gameManager.PauseGame();
        }

        public void NextLevelButton()
        {
            _gameManager.NextScene();
        }

        private void StartButton()
        {
            start.gameObject.SetActive(false);
            _gameManager.ResumeGame();
        }

        private void RetryButton()
        {
            _gameManager.RestartGame();
        }

        private void SetLevelText()
        {
            levelText.text = "LEVEL: " + _gameManager.level.ToString();
        }

        void StopAllAudio()
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
        }
    }
}