using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private AudioSource buttonClick;

    private void Start()
    {
        startButton.onClick.AddListener(LoadScene);
        quitButton.onClick.AddListener(QuitGame);
    }
    void LoadScene()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        buttonClick.Play();
        UnityEditor.EditorApplication.isPlaying = false;
#else
        buttonClick.Play();
        Application.Quit();
#endif
    }
}
