using System.Collections;
using UnityEngine;

public class AutoDoorController : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private GameObject doorObject;

    [Header("Delay for DoorOpen")]
    [SerializeField] private float doorOpenDelay = 5f; 

    private Animator doorAnimator;

    private void Start()
    {
        if (doorObject != null)
            doorAnimator = doorObject.GetComponent<Animator>();

        StartCoroutine(OpenDoorAfterDelay());
    }

    private IEnumerator OpenDoorAfterDelay()
    {
        yield return new WaitForSeconds(doorOpenDelay);

        doorAnimator.SetBool("isOpen", true);
        AudioActions.onToggleDoorAudio?.Invoke(true);
    }

}
