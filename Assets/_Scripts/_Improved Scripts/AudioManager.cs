using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject footstepsSFX;
    [SerializeField] private GameObject jumpSFX;
    [SerializeField] private GameObject pickupSFX;

    [Header("Door")]
    [SerializeField] private AudioSource doorOpenSFX;
    [SerializeField] private AudioSource doorCloseSFX;

    [Header("Lift")]
    [SerializeField] private AudioSource liftSFX;

    [Header("Cheatsheet")]
    [SerializeField] private AudioSource cheatsheetSFX;

    [Header("Button")]
    [SerializeField] private AudioSource buttonClickSFX;
    [SerializeField] private AudioSource uiButtonClickSFX;

    [Header("Screen")]
    [SerializeField] private AudioSource screenOnSFX;

    [Header("Light On")]
    [SerializeField] private AudioSource lightOnSFX;

    [Header("Light Ambiance")]
    [SerializeField] private AudioSource lightHumSFX;

    [Header("Fall/Wind")]
    [SerializeField] private AudioSource fallSFX;

    [Header("Ending")]
    [SerializeField] private AudioSource endBgm;

    private void TogglePlayerAudio(bool enabled)
    {
        footstepsSFX.SetActive(enabled);
        jumpSFX.SetActive(enabled);
        pickupSFX.SetActive(enabled);
    }

    private void ToggleDoorAudio(bool enabled)
    {
        if (enabled)
            doorOpenSFX.Play();
        else      
            doorCloseSFX.Play();   
    }
    private void ToggleFallAudio(bool enabled)
    { 
        if(enabled)
            fallSFX.Play();
        else 
            fallSFX.Stop();
    }

    private void ToggleLiftSound(bool enabled)
    {
        if(enabled)
            liftSFX.Play();
        else
            liftSFX.Stop();
    }

    private void ButtonClickAudio()
    {
        buttonClickSFX.Play();
    }

    private void uiButtonClickAudio()
    {
        uiButtonClickSFX.Play();
    }

    private void ScreenOnAudio() 
    {
        screenOnSFX.Play();
    }

    private void CheatsheetSound()
    {
        cheatsheetSFX.Play();
    }

    private void LightOnSound()
    {
        lightOnSFX.Play();
    }

    private void LightHumSound()
    {
        lightHumSFX.Play();
    }


    private void EndBgmAudio()
    {
        endBgm.Play();
    }

    private void OnEnable()
    {
        AudioActions.onTogglePlayerAudio += TogglePlayerAudio;
        AudioActions.onToggleDoorAudio += ToggleDoorAudio;
        AudioActions.onCheatSheetAudioPlay += CheatsheetSound;
        AudioActions.onButtonClickAudioPlay += ButtonClickAudio;
        AudioActions.onUiButtonClickAudioPlay += uiButtonClickAudio;
        AudioActions.onScreenAudioPlay += ScreenOnAudio;
        AudioActions.onToggleLiftAudioPlay += ToggleLiftSound;
        AudioActions.onLightAudioPlay += LightOnSound;
        AudioActions.onAmbiancePlay += LightHumSound;
        AudioActions.onToggleFallAudio += ToggleFallAudio;
        AudioActions.onEndBgmPlay += EndBgmAudio;
    }

    private void OnDisable()
    {
        AudioActions.onTogglePlayerAudio -= TogglePlayerAudio;
        AudioActions.onToggleDoorAudio -= ToggleDoorAudio;
        AudioActions.onCheatSheetAudioPlay -= CheatsheetSound;
        AudioActions.onButtonClickAudioPlay -= ButtonClickAudio;
        AudioActions.onUiButtonClickAudioPlay -= uiButtonClickAudio;
        AudioActions.onScreenAudioPlay -= ScreenOnAudio;
        AudioActions.onToggleLiftAudioPlay -= ToggleLiftSound;
        AudioActions.onLightAudioPlay -= LightOnSound;
        AudioActions.onAmbiancePlay -= LightHumSound;
        AudioActions.onToggleFallAudio -= ToggleFallAudio;
        AudioActions.onEndBgmPlay -= EndBgmAudio;
    }

}
