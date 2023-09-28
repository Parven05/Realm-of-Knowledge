using UnityEngine;

public class CheatsheetTrigger : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject cheatsheetCanvas;

    private bool isCheatsheetActive = false;
    private bool hasTriggered = false;

    private void Start()
    {
        cheatsheetCanvas.SetActive(false);
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
            CloseCheatsheetCanvas();
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
        AudioActions.onCheatSheetAudioPlay?.Invoke();
        AudioActions.onTogglePlayerAudio?.Invoke(false);
        GameActions.onToggleCursorState?.Invoke(true);

        Time.timeScale = 0f;
        isCheatsheetActive = true;
        cheatsheetCanvas.SetActive(true);

    }

    private void CloseCheatsheetCanvas()
    {
        GameActions.onToggleCursorState?.Invoke(false);
        AudioActions.onTogglePlayerAudio?.Invoke(true);

        Time.timeScale = 1f;
        isCheatsheetActive = false;
        cheatsheetCanvas.SetActive(false);
       
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
