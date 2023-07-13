using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenOnTrigger : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorTrigger;
    [SerializeField] private AudioSource doorOpenSfx;

    private void Start()
    {
        doorTrigger.SetActive(false);
    }
    private void DoorOpen()
    {
        doorTrigger.SetActive(true);
        doorOpenSfx.Play();
        doorAnimator.SetBool("isOpen", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorOpen();
        }
    }
}
