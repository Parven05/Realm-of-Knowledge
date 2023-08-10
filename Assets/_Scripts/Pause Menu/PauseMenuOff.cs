using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOff : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private PauseMenu pauseMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pauseMenu.hasTriggered = false;
            pauseMenuCanvas.SetActive(false);
        }
    }
}
