using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriflesGames.StreetGangs.Controllers
{
	public class WeaponController : MonoBehaviour
	{
		public bool isUsingKatana;
		public bool isUsingRifle;

		[SerializeField]
		private float katanaTime = 20;
		[SerializeField]
		private float rifleTime = 10;

		[SerializeField]
		private GameObject rifle;
		[SerializeField]
		private GameObject katana;

		private EnemyAnimationController[] enemyAnimation;

		void Start()
		{
			isUsingKatana = true;
			isUsingRifle = false;
			enemyAnimation = FindObjectsOfType<EnemyAnimationController>();
			StartCoroutine(KatanaTimer());
		}

		IEnumerator KatanaTimer()
		{
			ChangeAnimationState(EnemyAnimationController.enemyState.shooting);
			yield return new WaitForSeconds(katanaTime);
			isUsingKatana = false;
			isUsingRifle = true;
			katana.SetActive(false);
			rifle.SetActive(true);
			StartCoroutine(RifleTimer());
		}

		IEnumerator RifleTimer()
		{
			ChangeAnimationState(EnemyAnimationController.enemyState.hiding);
			yield return new WaitForSeconds(rifleTime);
			isUsingKatana = true;
			isUsingRifle = false;
			katana.SetActive(true);
			rifle.SetActive(false);
			StartCoroutine(KatanaTimer());
		}

		private void ChangeAnimationState(EnemyAnimationController.enemyState state)
        {
            for (int i = 0; i < enemyAnimation.Length; i++)
            {
				enemyAnimation[i].ChangeAnimation(state);
            }
        } 
	}
}