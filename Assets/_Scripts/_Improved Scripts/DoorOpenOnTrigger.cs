using UnityEngine;

public class DoorOpenOnTrigger : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorTrigger;

    private bool doorOpened = false; 

    private void Start()
    {
        doorTrigger.SetActive(false);
    }

    private void DoorOpen()
    {
        doorTrigger.SetActive(false);
        AudioActions.onToggleDoorAudio?.Invoke(true);
        doorAnimator.SetBool("isOpen", true);
        doorOpened = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!doorOpened && other.CompareTag("Player"))
        {
            DoorOpen();
        }
    }
    
    
}
