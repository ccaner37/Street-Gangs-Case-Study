using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TriflesGames.StreetGangs.Controllers;


namespace TriflesGames.StreetGangs.Managers
{
	public class GameManager : MonoBehaviour
	{
		private UIManager _uiManager;

		public int level;

		private static GameManager instance = null;
		public static GameManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GameObject("GameManager").AddComponent<GameManager>();
				}

				return instance;
			}
		}

		private void Start()
		{
			level = SceneManager.GetActiveScene().buildIndex + 1;
			_uiManager = FindObjectOfType<UIManager>();
		}

		private void OnEnable()
		{
			instance = this;
			PauseGame();
		}

		public void PauseGame()
		{
			Time.timeScale = 0;
			Cursor.lockState = CursorLockMode.None;
		}

		public void ResumeGame()
		{
			Time.timeScale = 1;
			Cursor.lockState = CursorLockMode.Locked;
		}

		public void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void NextScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		public void IsEveryoneDead()
		{
			FindObjectOfType<EnemyHealthController>().aliveEnemies -= 1;
			var aliveEnemiesNumber = FindObjectOfType<EnemyHealthController>().aliveEnemies;

			if (aliveEnemiesNumber == 0)
			{
				StartCoroutine(GameEnd());
			}
		}

		IEnumerator GameEnd()
		{
			yield return new WaitForSeconds(1);
			_uiManager.LevelCompleted();
		}
	}
}