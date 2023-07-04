using UnityEngine;

public class PuzzleDoorOpener : MonoBehaviour
{
    [SerializeField] private QuizCompletionHandler[] completionObjects;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource doorOpenSfx;

    private bool allCompleted = false;

    private void Start()
    {
        foreach (var completionObject in completionObjects)
        {
            completionObject.OnQuizCompleted += HandleQuizCompleted;
        }
    }

    private void OnDestroy()
    {
        foreach (var completionObject in completionObjects)
        {
            completionObject.OnQuizCompleted -= HandleQuizCompleted;
        }
    }

    private void HandleQuizCompleted()
    {
        foreach (var completionObject in completionObjects)
        {
            if (!completionObject.IsCompleted)
            {
                return; // Not all completion objects are complete
            }
        }

        if (!allCompleted)
        {
            doorOpenSfx.Play();
            doorAnimator.SetBool("isOpen", true);
            allCompleted = true;
        }
    }
}
