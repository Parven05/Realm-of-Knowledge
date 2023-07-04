using UnityEngine;

public class PuzzleDoorOpener : MonoBehaviour
{
    [SerializeField] private QuizCompletionHandler[] completionObjects;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorTrigger;
    [SerializeField] private AudioSource doorOpenSfx;
    [SerializeField] private GameObject purpleCube;

    private bool allCompleted = false;

    private void Start()
    {
        foreach (var completionObject in completionObjects)
        {
            completionObject.OnQuizCompleted += HandleQuizCompleted;
        }

        doorTrigger.SetActive(false);
        doorAnimator.SetBool("isOpen", false); // Ensure the door starts closed
        purpleCube.layer = LayerMask.NameToLayer("Default"); // Set the cube's initial layer
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

    private void CubeEnabled()
    {
        purpleCube.layer = LayerMask.NameToLayer("pickup"); // Change the cube's layer to allow pickup
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
            CubeEnabled();
            allCompleted = true;
        }
    }
}
