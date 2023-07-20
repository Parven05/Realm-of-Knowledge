using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    private void Start()
    {
        startButton.onClick.AddListener(LoadScene);
    }
    void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }
}
