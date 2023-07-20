using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource introSoundEffect;
    [SerializeField] float introDelay = 22f;

    private bool isIntroPlayed = false;

    private void Start()
    {
        PlayIntroSoundEffectWithDelay();
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
