using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenOnTrigger : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorTrigger;
    [SerializeField] private AudioSource doorOpenSfx;

    private bool doorOpened = false; // New flag to check if the door is already opened.

    private void Start()
    {
        doorTrigger.SetActive(false);
    }

    private void DoorOpen()
    {
        doorTrigger.SetActive(false);
        doorOpenSfx.Play();
        doorAnimator.SetBool("isOpen", true);
        doorOpened = true; // Set the flag to true when the door is opened.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!doorOpened && other.CompareTag("Player")) // Check if the door is not already opened and if the player triggers the collider.
        {
            DoorOpen();
        }
    }
}
