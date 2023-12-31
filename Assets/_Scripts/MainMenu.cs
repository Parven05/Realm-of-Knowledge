using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Items")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [Header("Start Items")]
    [SerializeField] private Button classicButton;
    [SerializeField] private Button funButton;
    [SerializeField] private Button StartBackButton;

    [Header("Menu Music & Sound Effects")]
    [SerializeField] private AudioSource menuBGM;
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private float musicFadeDuration = 1.5f;

    [Header("Animation After Clicked Buttons")]
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject mainCamera;

    [Header("Loading Screen Classic")]
    public GameObject loadingScreen;
    public Slider loadingSlider;

    [Header("Loading Screen Fun")]
    public GameObject loadingFunScreen;
    public Slider loadingFunSlider;

    [Header("Light Activation")]
    [SerializeField] private GameObject lightsToActivate;
    [SerializeField] private Material emissionMaterials;
    [SerializeField] private AudioSource lightOnSfx;
    [SerializeField] private float activationDelay;

    [Header("Canvas")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject creditCanvas;
    [SerializeField] private GameObject startCanvas;

    private Animator panelAnim;
    private Animator mainCameraAnim;

    private void Start()
    {
        lightsToActivate.SetActive(false);
        emissionMaterials.DisableKeyword("_EMISSION");

        panel.SetActive(false);
        creditCanvas.SetActive(false);
        startCanvas.SetActive(false);

        panelAnim = panel.GetComponent<Animator>();
        mainCameraAnim = mainCamera.GetComponent<Animator>();

        startButton.onClick.AddListener(LoadScene);
        creditButton.onClick.AddListener(LoadToCredit);
        quitButton.onClick.AddListener(QuitGame);
        backButton.onClick.AddListener(LoadToMenu);

        classicButton.onClick.AddListener(LoadToGameScene);
        funButton.onClick.AddListener(LoadToFunScene);

        StartBackButton.onClick.AddListener(BackToMenu);
    }

    void PlayPanelAnimation()
    {
        panel.SetActive(true);
        panelAnim.SetBool("isFade", true);
    }

    void LoadToGameScene()
    {
        buttonClick.Play();
        startCanvas.SetActive(false);
        PlayPanelAnimation();
        StartCoroutine(FadeOutMusicAndLoadSceneClassic());
    }


    void LoadToFunScene()
    {
        buttonClick.Play();
        startCanvas.SetActive(false);
        PlayPanelAnimation();
        StartCoroutine(FadeOutMusicAndLoadSceneFun());
    }

    void BackToMenu()
    {
        buttonClick.Play();
        startCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    void LoadToMenu()
    {
        buttonClick.Play();
        creditCanvas.SetActive(false);
        LightOffCredit();
        mainCameraAnim.SetBool("isRight", false);
        menuCanvas.SetActive(true);
    }

    void LoadToCredit()
    {
        buttonClick.Play();
        menuCanvas.SetActive(false);
        mainCameraAnim.SetBool("isRight", true);
        creditCanvas.SetActive(true);
        StartCoroutine(LightOnCredit());
    }

    void LoadScene()
    {
        buttonClick.Play();
        startCanvas.SetActive(true);
        menuCanvas.SetActive(false);


    }

    void QuitGame()
    {
#if UNITY_EDITOR
        buttonClick.Play();
        UnityEditor.EditorApplication.isPlaying = false;
#else
        buttonClick.Play();
        Application.Quit();
#endif
    }

    private IEnumerator LightOnCredit()
    {
        yield return new WaitForSeconds(activationDelay);
        lightOnSfx.Play();
        lightsToActivate.SetActive(true);
        emissionMaterials.EnableKeyword("_EMISSION");
    }

    void LightOffCredit()
    {
        //lightOnSfx.Play();
        lightsToActivate.SetActive(false);
        emissionMaterials.DisableKeyword("_EMISSION");
    }

    private IEnumerator FadeOutMusicAndLoadSceneClassic()
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
    private IEnumerator FadeOutMusicAndLoadSceneFun()
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
        loadingFunScreen.SetActive(true);

        // Begin loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Fun");

        // Update the loading progress
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // The progress goes from 0 to 0.9, so we normalize it to 0 to 1
            loadingFunSlider.value = progress;
            yield return null;
        }
    }
}
