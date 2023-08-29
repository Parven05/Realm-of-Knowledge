using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject backToMenuCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private GameObject settingsCanvas;

 /*   [Header("Button")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button controlButton;
    [SerializeField] private Button controlBackButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button settingsBackButton; */
    
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;

    public bool hasTriggered;

    private void Start()
    {
        hasTriggered = false;
        pauseMenuCanvas.SetActive(false);

       /* pauseButton.onClick.AddListener(NextLoad);
        mainMenuButton.onClick.AddListener(BackToMainMenu);
        resumeButton.onClick.AddListener(Resume);

        controlButton.onClick.AddListener(GoToControlsMenu);
        controlBackButton.onClick.AddListener(ControlsMenuBack);

        settingsButton.onClick.AddListener(GoToSettingsMenu);
        settingsBackButton.onClick.AddListener(SettingsMenuBack);*/

    }

    void NextLoad()
    {
        AudioActions.onUiButtonClickAudioPlay?.Invoke();
        pauseMenuCanvas.SetActive(false);
        backToMenuCanvas.SetActive(true);
    }

    void PlayerInteraction(bool enabled)
    {
        GameActions.onToggleCursorState?.Invoke(enabled);
        GameActions.onToggleCameraState?.Invoke(enabled);
        AudioActions.onTogglePlayerAudio?.Invoke(enabled);    
    }

    void Resume()
    {
        AudioActions.onUiButtonClickAudioPlay?.Invoke();
        Time.timeScale = 1f;
        PlayerInteraction(true);
        SetCursorState(false);
        backToMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    private void SetCursorState(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }
    void Update()
    {
        if (hasTriggered)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0f;
                SetCursorState(true);
                PlayerInteraction(false);
            }
        }
        
    }

    void GoToControlsMenu()
    {
        AudioActions.onUiButtonClickAudioPlay?.Invoke();
        backToMenuCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    void GoToSettingsMenu()
    {
        AudioActions.onUiButtonClickAudioPlay?.Invoke();
        backToMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }    

    void ControlsMenuBack()
    {
        AudioActions.onUiButtonClickAudioPlay?.Invoke();
        controlsCanvas.SetActive(false);
        backToMenuCanvas.SetActive(true);
    }

    void SettingsMenuBack()
    {
        AudioActions.onUiButtonClickAudioPlay?.Invoke();
        settingsCanvas.SetActive(false);
        backToMenuCanvas.SetActive(true);
    }

    void BackToMainMenu()
    {
        AudioActions.onUiButtonClickAudioPlay?.Invoke();
        backToMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(LoadingScreentoMenu());
    }

    private IEnumerator LoadingScreentoMenu()
    {
        loadingScreen.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main Menu");

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            pauseMenuCanvas.SetActive(true);
        }
    }
}
