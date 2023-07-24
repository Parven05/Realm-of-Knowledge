using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private GameObject doorObject;
    [SerializeField] private AudioSource doorOpenSfx;
    [SerializeField] private AudioSource doorCloseSfx;

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
            doorOpenSfx.Play();
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            doorAnimator.SetBool("isOpen", false);
            doorCloseSfx.Play();
            isOpen = false;
        }
    }
}
