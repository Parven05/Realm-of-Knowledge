using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doorTrigger;

    private void Start()
    {
        doorTrigger.SetActive(true);
    }
    private void DoorClose()
    {
        doorTrigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorClose();
        }
    }
}
