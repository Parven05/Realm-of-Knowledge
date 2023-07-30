using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject backToMenuCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject controlsCanvas;

    [Header("Button")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button controlButton;
    [SerializeField] private Button controlBackButton;
    [SerializeField] private AudioSource buttonClickedSFX;

    [Header("Player")]
    [SerializeField] private GameObject playerCursor;
    [SerializeField] private FirstPersonController player;
    [SerializeField] private GameObject footstepsSFX;
    [SerializeField] private GameObject jumpSfx;
    
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;

    public bool hasTriggered;
    private bool hasPressedEscapeOnce = false;

    private void Start()
    {
        hasTriggered = false;
        pauseMenuCanvas.SetActive(false);

        pauseButton.onClick.AddListener(NextLoad);
        mainMenuButton.onClick.AddListener(BackToMainMenu);
        resumeButton.onClick.AddListener(Resume);

        controlButton.onClick.AddListener(GoToControlsMenu);
        controlBackButton.onClick.AddListener(ControlsMenuBack);
    }

    void NextLoad()
    {
        buttonClickedSFX.Play();
        pauseMenuCanvas.SetActive(false);
        backToMenuCanvas.SetActive(true);
    }

    void PlayerInteraction(bool enabled)
    {
        playerCursor.SetActive(enabled);
        player.cameraCanMove = enabled;
        footstepsSFX.SetActive(enabled);
        jumpSfx.SetActive(enabled);
    }

    void Resume()
    {
        buttonClickedSFX.Play();
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
                if (!hasPressedEscapeOnce)
                {
                    // First time pressing Escape
                    Time.timeScale = 0f;
                    SetCursorState(true);
                    PlayerInteraction(false);
                    hasPressedEscapeOnce = true;
                }
                else
                {
                    // Second time pressing Escape
                    Time.timeScale = 1f;
                    SetCursorState(false);
                    PlayerInteraction(true);
                    hasPressedEscapeOnce = false; // Reset for the next time
                }
            }
        }
        
    }

    void GoToControlsMenu()
    {
        buttonClickedSFX.Play();
        backToMenuCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    void ControlsMenuBack()
    {
        buttonClickedSFX.Play();
        controlsCanvas.SetActive(false);
        backToMenuCanvas.SetActive(true);
    }

    void BackToMainMenu()
    {
        buttonClickedSFX.Play();
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
