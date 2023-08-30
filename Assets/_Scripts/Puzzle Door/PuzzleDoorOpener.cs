using UnityEngine;

public class PuzzleDoorOpener : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorTrigger;

    [Header("Required Object")]
    [SerializeField] private QuizCompletionHandler[] completionObjects;

    private bool allCompleted = false;
    private bool doorOpened = false; // New flag to check if the door is already opened.

    private void Start()
    {
        foreach (var completionObject in completionObjects)
        {
            completionObject.OnQuizCompleted += HandleQuizCompleted;
        }

        doorTrigger.SetActive(false);
        doorAnimator.SetBool("isOpen", false); // Ensure the door starts closed
    }

    private void OnDestroy()
    {
        foreach (var completionObject in completionObjects)
        {
            completionObject.OnQuizCompleted -= HandleQuizCompleted;
        }
    }

    private void DoorOpen()
    {
        doorTrigger.SetActive(false);

        // Play the door open sound only if the door is not already opened.
        if (!doorOpened)
        {
            AudioActions.onToggleDoorAudio?.Invoke(true);
            doorOpened = true;
        }

        doorAnimator.SetBool("isOpen", true);
    }

    private void HandleQuizCompleted()
    {
        foreach (var completionObject in completionObjects)
        {
            if (!completionObject.IsCompleted)
            {
                return;
            }
        }

        if (!allCompleted)
        {
            DoorOpen();
            allCompleted = true;
        }
    }
}
