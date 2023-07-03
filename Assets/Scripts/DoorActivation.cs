using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    [SerializeField] private GameObject doorObject;
    [SerializeField] private AudioSource doorOpenSfx;
    [SerializeField] private AudioSource doorCloseSfx;
    private Animator doorAnimate;

    private bool doorActivated = false;

    private void Start()
    {
        if (doorObject != null)
            doorAnimate = doorObject.GetComponent<Animator>();
        else
            Debug.LogError("Door object is not assigned!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoorActivationCube") && !doorActivated)
        {
            
            doorAnimate.SetBool("isOpen", true);
            doorOpenSfx.Play();
            doorActivated = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoorActivationCube") && doorActivated)
        {
            doorAnimate.SetBool("isOpen", false);
            doorCloseSfx.Play();
            doorActivated = false;
    
        }
    }
}
