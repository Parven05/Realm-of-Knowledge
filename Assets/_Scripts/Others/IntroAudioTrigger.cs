using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource introAudioSfx;
    [SerializeField] private float delayInSeconds = 2.0f;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasPlayed)
        {
            StartCoroutine(PlayIntroAudioWithDelay());
            hasPlayed = true;
        }
    }

    private IEnumerator PlayIntroAudioWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        introAudioSfx.Play();
    }
}
