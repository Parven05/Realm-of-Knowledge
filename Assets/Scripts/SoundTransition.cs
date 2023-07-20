using UnityEngine;
using System.Collections;

public class SoundTransition : MonoBehaviour
{
    [SerializeField] private AudioSource previousBGMSource;
    [SerializeField] private AudioSource newBGMSource;
    [SerializeField] private float fadeOutDuration = 2.0f;
    [SerializeField] private float fadeInDuration = 2.0f;

    private bool hasTransitioned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTransitioned && other.CompareTag("Player"))
        {
            TriggerMusicTransition();
        }
    }

    public void TriggerMusicTransition()
    {
        if (hasTransitioned)
            return;

        StartCoroutine(TransitionMusic());
    }

    IEnumerator TransitionMusic()
    {
        hasTransitioned = true;

        // Fade out previous BGM
        float startVolume = previousBGMSource.volume;
        while (previousBGMSource.volume > 0)
        {
            previousBGMSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }
        previousBGMSource.Stop();

        // Fade in new BGM
        newBGMSource.Play();
        newBGMSource.volume = 0;
        while (newBGMSource.volume < 1)
        {
            newBGMSource.volume += Time.deltaTime / fadeInDuration;
            yield return null;
        }
    }
}
