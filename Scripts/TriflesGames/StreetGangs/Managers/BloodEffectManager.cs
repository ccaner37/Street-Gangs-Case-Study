using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TriflesGames.StreetGangs.Managers
{
    public class BloodEffectManager : MonoBehaviour
    {
        private float duration = 0f;
        private float transAmount = 0.50f;
        private float decreaseFactor = 0.20f;

        private void Update()
        {
            BloodEffect();
        }

        private void BloodEffect()
        {
            if (duration > 0)
            {
                Image bloodEffect = gameObject.GetComponent<Image>();
                Color tempColor = bloodEffect.color;
                tempColor.a -= 0.005f;
                bloodEffect.color = tempColor;

                duration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                duration = 0;
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            duration = 0.15f;
            Image bloodEffect = gameObject.GetComponent<Image>();
            Color tempColor = bloodEffect.color;
            tempColor.a = transAmount;
            bloodEffect.color = tempColor;
        }
    }
}
