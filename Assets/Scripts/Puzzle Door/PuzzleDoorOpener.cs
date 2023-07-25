using UnityEngine;

public class PuzzleDoorOpener : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorTrigger;
    [SerializeField] private AudioSource doorOpenSfx;

    [Header("Required Object")]
    [SerializeField] private QuizCompletionHandler[] completionObjects;
   
    private bool allCompleted = false;

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
        doorTrigger.SetActive(true);
        doorOpenSfx.Play();
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
