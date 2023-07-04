using UnityEngine;

public class PuzzleDoorOpener : MonoBehaviour
{
    [SerializeField] private QuizCompletionHandler[] completionObjects;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource doorOpenSfx;

    [SerializeField] private CageOpener cageOpener;

  

    private bool allCompleted = false;

    private void Start()
    {
        foreach (var completionObject in completionObjects)
        {
            completionObject.OnQuizCompleted += HandleQuizCompleted;
        }

        cageOpener.enabled = false;
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
            cageOpener.enabled = true;
            allCompleted = true;
        }
    }
}
