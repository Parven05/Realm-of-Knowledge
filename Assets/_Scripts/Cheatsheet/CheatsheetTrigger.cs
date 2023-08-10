using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsheetTrigger : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject cheatsheetCanvas;
    [SerializeField] private AudioSource cheatsheetSFX;

    [Header("Dependencies")]
    [SerializeField] private FirstPersonController player;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject footstepsSFX;
    [SerializeField] private GameObject jumpSFX;

    private bool isCheatsheetActive = false;
    private bool hasTriggered = false;

    private void Start()
    {
        cheatsheetCanvas.SetActive(false);
    }

    private void PlayerInteraction(bool enabled)
    {

        crosshair.SetActive(enabled);
        player.cameraCanMove = enabled;
        footstepsSFX.SetActive(enabled);
        jumpSFX.SetActive(enabled);

    }

    private void SetCursorState(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasTriggered = false;
            CloseCheatsheetCanvas(); // Close the canvas when the player exits the trigger.
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && hasTriggered)
        {
            ToggleCheatsheetCanvas();
            
        }
    }

    private void OpenCheatsheetCanvas()
    {
        cheatsheetSFX.Play();
        Time.timeScale = 0f;
        isCheatsheetActive = true;
        cheatsheetCanvas.SetActive(true);
        SetCursorState(true);
        PlayerInteraction(false);
    }

    private void CloseCheatsheetCanvas()
    {
        Time.timeScale = 1f;
        isCheatsheetActive = false;
        cheatsheetCanvas.SetActive(false);
        SetCursorState(false);
        PlayerInteraction(true);
    }

    private void ToggleCheatsheetCanvas()
    {
        if (isCheatsheetActive)
        {
            CloseCheatsheetCanvas();
            
        }
        else
        {
            OpenCheatsheetCanvas();
           
        }
    }
}
