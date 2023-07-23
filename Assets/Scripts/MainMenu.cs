using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private AudioSource menuBGM;
    [SerializeField] private GameObject panel;
    [SerializeField] private float musicFadeDuration = 1.5f;

    [SerializeField] private AudioSource buttonClick;

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

       
        SceneManager.LoadScene("Game");
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

       
        UnityEditor.EditorApplication.isPlaying = false;
    }
#endif
}
