using UnityEngine;

public class DoorCloseOnTrigger : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private GameObject doorObject;

    private Animator doorAnimator;
    private bool isDoorOpen = true;

    private void Start()
    {
        if (doorObject != null)
            doorAnimator = doorObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isDoorOpen)
        {
            doorAnimator.SetBool("isOpen", false);
            AudioActions.onToggleDoorAudio?.Invoke(false);
            isDoorOpen = false;
        }
    }
}
