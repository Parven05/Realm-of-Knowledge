using UnityEngine;

public class IntroBGM : MonoBehaviour
{
    [Header("Intro Music")]
    [SerializeField] private AudioSource introSoundEffect;
    [SerializeField] float introDelay = 22f;

    private void Start()
    {
        PlayIntroSoundEffectWithDelay();
    }

    private void PlayIntroSoundEffectWithDelay()
    {
        if (introSoundEffect != null)
        {
            introSoundEffect.PlayDelayed(introDelay);
        }
    }
}
