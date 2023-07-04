using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpener : MonoBehaviour
{
    [SerializeField] private Animator cageAnimator;
    // [SerializeField] private AudioSource cageOpenSfx;

    private void Start()
    {
        cageAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            cageAnimator.SetBool("isOpen", true);
        }
    }

}
