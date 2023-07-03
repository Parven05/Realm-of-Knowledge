using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource introSoundEffect;
    public float introDelay = 2f;

    private bool isIntroPlayed = false;

    private void Start()
    {
        PlayIntroSoundEffectWithDelay();
    }

    private void Update()
    {
        // Check if the intro sound effect has finished playing
        if (isIntroPlayed && !introSoundEffect.isPlaying)
        {
           
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

}
