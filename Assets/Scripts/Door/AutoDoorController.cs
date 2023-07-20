using UnityEngine;

public class AutoDoorController : MonoBehaviour
{
    [SerializeField] private GameObject doorObject; 
    [SerializeField] private AudioSource doorOpenSfx;
    [SerializeField] private AudioSource doorCloseSfx;
    [SerializeField] private float doorOpenDelay = 5f; 

    private Animator doorAnimator;
    private bool isOpen = false;

    private void Start()
    {
        if (doorObject != null)
            doorAnimator = doorObject.GetComponent<Animator>();

        StartCoroutine(OpenDoorAfterDelay());
    }

    private System.Collections.IEnumerator OpenDoorAfterDelay()
    {
        yield return new WaitForSeconds(doorOpenDelay);
        doorAnimator.SetBool("isOpen", true);
        doorOpenSfx.Play();
        isOpen = true;
    }

    private void CloseDoor()
    {
        doorAnimator.SetBool("isOpen", false);
        doorCloseSfx.Play();
        isOpen = false;
    }
}
