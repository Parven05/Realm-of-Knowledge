using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorObject; // Reference to the GameObject with the Animator component

    private Animator doorAnimator;
    private bool isOpen = false;

    private void Start()
    {
        if (doorObject != null)
            doorAnimator = doorObject.GetComponent<Animator>();
        else
            Debug.LogError("Door object is not assigned!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            doorAnimator.SetBool("isOpen", true);
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            doorAnimator.SetBool("isOpen", false);
            isOpen = false;
        }
    }
}