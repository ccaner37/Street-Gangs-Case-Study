using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriflesGames.StreetGangs.Controllers
{
    public class KatanaSlicer : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip[] audioClips;

        private void OnEnable()
        {
            audioSource.PlayOneShot(audioClips[0]);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                collision.gameObject.GetComponent<EnemyBulletController>().SliceBullet();
                PlayRandomSound();
            }
        }

        private void PlayRandomSound()
        {
            int randomIndex = Random.Range(1, audioClips.Length);
            AudioClip randomAudioClip = audioClips[randomIndex];
            audioSource.PlayOneShot(randomAudioClip);
        }
    }
}