using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private GameObject doorObject;

    private Animator doorAnimator;
    private bool isOpen = false;
    private void Start()
    {
        if (doorObject != null)
            doorAnimator = doorObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            doorAnimator.SetBool("isOpen", true);
            AudioActions.onToggleDoorAudio?.Invoke(true);
            isOpen = true;
        }
    }

}
