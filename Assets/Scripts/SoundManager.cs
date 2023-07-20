using UnityEngine;
using System.Collections;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource introSoundEffect;
    [SerializeField] private AudioSource introSoundBGM;
    [SerializeField] private float introDelay = 2f;
    [SerializeField] private float fadeDuration = 2f;

    private bool isIntroPlayed = false;

    private void Start()
    {
        PlayIntroSoundEffectWithDelay();
    }

    private void Update()
    {
        if (isIntroPlayed && !introSoundEffect.isPlaying && !introSoundBGM.isPlaying)
        {
            StartCoroutine(FadeInBGM());
        }
    }

    private void PlayIntroSoundEffectWithDelay()
    {
        if (introSoundEffect != null)
        {
            introSoundEffect.PlayDelayed(introDelay);
            isIntroPlayed = true;
        }
    }

    private IEnumerator FadeInBGM()
    {
        float currentTime = 0f;
        float startVolume = 0f;

        introSoundBGM.Play();
        introSoundBGM.volume = startVolume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            introSoundBGM.volume = Mathf.Lerp(startVolume, 1f, currentTime / fadeDuration);
            yield return null;
        }

        introSoundBGM.volume = 1f;
    }
}
