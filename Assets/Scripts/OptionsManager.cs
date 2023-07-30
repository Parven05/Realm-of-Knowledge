using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Music")]
    [SerializeField] private Slider volumeSlider;

    [Header("Game")]
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Slider fpsCapSlider; // Add the reference to the FPS cap slider
    [SerializeField] private TextMeshProUGUI fpsCapText;

    [Header("Button")]
    [SerializeField] private Button applyButton;
    [SerializeField] private AudioSource buttonClickedSFX;

    private int baseFPS = 60; // Set the base FPS value (starting from 60)
    private int midFPS = 120; // Set the mid FPS value (120 FPS)
    private int maxFPS = 999; // Set the maximum FPS value (unlimited)

    private Resolution[] resolutions;

    private void Start()
    {
        // Get available screen resolutions and populate the resolution dropdown with options
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        foreach (var resolution in resolutions)
        {
            resolutionOptions.Add(resolution.width + "x" + resolution.height);
        }
        resolutionDropdown.AddOptions(resolutionOptions);

        // Set the default selected options for the dropdowns and toggle
        resolutionDropdown.value = GetCurrentResolutionIndex();
        resolutionDropdown.RefreshShownValue();
        fullscreenToggle.isOn = Screen.fullScreen;

        // Add listeners to the buttons, sliders, and toggle to call the corresponding methods when clicked or value changed
        applyButton.onClick.AddListener(ApplyChanges);
        fullscreenToggle.onValueChanged.AddListener(OnToggleValueChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeValueChanged);
        vsyncToggle.onValueChanged.AddListener(OnVSyncToggleChanged);
        fpsCapSlider.onValueChanged.AddListener(OnFPSCapValueChanged);

        // Set the initial FPS cap value in the text element
        fpsCapText.text = fpsCapSlider.value.ToString("F0");
        SetFPSCapSliderValue(baseFPS);
    }

    private void ApplyChanges()
    {
        buttonClickedSFX.Play();
        // Get the selected display mode (fullscreen or windowed) from the toggle
        bool isFullscreen = fullscreenToggle.isOn;

        // Set the resolution based on the selected option in the resolutionDropdown
        int selectedResolutionIndex = resolutionDropdown.value;
        Resolution selectedResolution = resolutions[selectedResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullscreen);
    }

    private void OnToggleValueChanged(bool newValue)
    {
        // If the toggle value changed, apply the changes immediately
        ApplyChanges();
    }

    private void OnVolumeValueChanged(float newValue)
    {
        // Adjust the game volume based on the slider value
        AudioListener.volume = newValue;
    }

    private void OnVSyncToggleChanged(bool newValue)
    {
        // Enable or disable VSync based on the toggle value
        QualitySettings.vSyncCount = newValue ? 1 : 0;
    }

    private void SetFPSCapSliderValue(int fpsCap)
    {
        // Update the FPS cap text element with the selected FPS value
        fpsCapText.text = fpsCap.ToString();

        // Update the FPS cap slider value to match the selected FPS value
        if (fpsCap == baseFPS)
        {
            fpsCapSlider.value = 0f;
        }
        else if (fpsCap == midFPS)
        {
            fpsCapSlider.value = 0.5f;
        }
        else
        {
            fpsCapSlider.value = 1f;
        }
    }

    private void OnFPSCapValueChanged(float newValue)
    {
        // Calculate the new FPS cap based on the slider value
        int newFPSCap = baseFPS;

        if (newValue >= 0.5f)
        {
            newFPSCap = maxFPS; // Slider at the maximum value, set unlimited FPS
        }
        else if (newValue > 0f)
        {
            newFPSCap = midFPS; // Slider in the middle, set 120 FPS
        }

        // Update the FPS cap text element with the selected FPS value
        fpsCapText.text = newFPSCap.ToString();

        // Apply the selected FPS cap
        Application.targetFrameRate = newFPSCap;
    }


    private int GetCurrentResolutionIndex()
    {
        // Get the current screen resolution and find its index in the resolutions array
        Resolution currentResolution = Screen.currentResolution;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width && resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }

        // If the current resolution is not found in the array, return the last resolution index (usually the highest resolution)
        return resolutions.Length - 1;
    }
}
