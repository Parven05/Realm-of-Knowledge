using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseOnTrigger : MonoBehaviour
{
   // [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorTrigger;
   // [SerializeField] private AudioSource doorOpenSfx;

    private void Start()
    {
        doorTrigger.SetActive(true);
    }
    private void DoorClose()
    {
        doorTrigger.SetActive(false);
       // doorOpenSfx.Play();
       // doorAnimator.SetBool("isOpen", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorClose();
        }
    }
}
