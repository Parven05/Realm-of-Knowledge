using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Items")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    [Header("Menu Music & Sound Effects")]
    [SerializeField] private AudioSource menuBGM;
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private float musicFadeDuration = 1.5f;

    [Header("Animation After Clicked Buttons")]
    [SerializeField] private GameObject panel;
    
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Slider loadingSlider;

    private Animator panelAnim;

    private void Start()
    {
        panel.SetActive(false);
        panelAnim = panel.GetComponent<Animator>();
        startButton.onClick.AddListener(LoadScene);
        quitButton.onClick.AddListener(QuitGame);
    }

    void PlayPanelAnimation()
    {
        panel.SetActive(true);
        panelAnim.SetBool("isFade", true);
    }

    void LoadScene()
    {
        buttonClick.Play();
        PlayPanelAnimation();
        StartCoroutine(FadeOutMusicAndLoadScene());
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        buttonClick.Play();
        StartCoroutine(FadeOutMusicAndQuitEditor());
#else
        buttonClick.Play();
        StartCoroutine(FadeOutMusicAndQuitApplication());
#endif
    }

    private IEnumerator FadeOutMusicAndLoadScene()
    {
        float startVolume = menuBGM.volume;
        float timer = 0f;

        while (timer < musicFadeDuration)
        {
            timer += Time.deltaTime;
            menuBGM.volume = Mathf.Lerp(startVolume, 0f, timer / musicFadeDuration);
            yield return null;
        }

        // Show the loading screen
        loadingScreen.SetActive(true);

        // Begin loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");

        // Update the loading progress
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // The progress goes from 0 to 0.9, so we normalize it to 0 to 1
            loadingSlider.value = progress;
            yield return null;
        }
    }

    private IEnumerator FadeOutMusicAndQuitApplication()
    {
        float startVolume = menuBGM.volume;
        float timer = 0f;

        while (timer < musicFadeDuration)
        {
            timer += Time.deltaTime;
            menuBGM.volume = Mathf.Lerp(startVolume, 0f, timer / musicFadeDuration);
            yield return null;
        }

        // Quit the application
        Application.Quit();
    }

#if UNITY_EDITOR
    private IEnumerator FadeOutMusicAndQuitEditor()
    {
        float startVolume = menuBGM.volume;
        float timer = 0f;

        while (timer < musicFadeDuration)
        {
            timer += Time.deltaTime;
            menuBGM.volume = Mathf.Lerp(startVolume, 0f, timer / musicFadeDuration);
            yield return null;
        }

        // Quit the editor
        UnityEditor.EditorApplication.isPlaying = false;
    }
#endif
}
