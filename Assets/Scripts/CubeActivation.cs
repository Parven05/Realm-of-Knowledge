using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeActivation : MonoBehaviour
{
    [SerializeField] private GameObject cubeObject;
    [SerializeField] private string newCubeTag; 

    private Renderer cubeRenderer;
    private bool cubeActivated = false;

    private void Start()
    {
        cubeRenderer = cubeObject.GetComponent<Renderer>();
    }

    private void SetCubeColor(Color color)
    {
        cubeRenderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!cubeActivated)
        {
            cubeActivated = true;
            cubeObject.tag = newCubeTag; 
            SetCubeColor(Color.black);
        }
    }
}
