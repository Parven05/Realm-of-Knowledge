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

    [Header("Cheatsheet")]
    [SerializeField] private AudioSource cheatsheetSFX;

    [Header("Button")]
    [SerializeField] private AudioSource buttonClickSFX;
    [SerializeField] private AudioSource uiButtonClickSFX;

    [Header("Screen")]
    [SerializeField] private AudioSource screenOnSFX;

    [Header("Light Ambiance")]
    [SerializeField] private AudioSource lightHumSFX;

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
        AudioActions.onAmbiancePlay += LightHumSound;
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
        AudioActions.onAmbiancePlay -= LightHumSound;
        AudioActions.onEndBgmPlay -= EndBgmAudio;
    }

}
