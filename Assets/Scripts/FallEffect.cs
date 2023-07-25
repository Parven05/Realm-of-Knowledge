using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEffect : MonoBehaviour
{
    [SerializeField] private AudioSource fallSFX;

    private bool isFalling = false;
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player") && !isFalling)
        {
            fallSFX.Play();
            isFalling = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isFalling)
        {
            fallSFX.Stop();
            isFalling = false;
        }
    }
}
