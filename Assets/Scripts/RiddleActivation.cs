using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleActivation : MonoBehaviour
{
    [SerializeField] private string cubeTag;
    [SerializeField] private Animator buttonAnimate;
    [SerializeField] private AudioSource buttonClickSfx;

    private Renderer buttonRenderer;
    private bool buttonClicked = false;

    private void Start()
    {
        buttonRenderer = GetComponent<Renderer>();
        buttonAnimate = GetComponent<Animator>();
        buttonAnimate.SetBool("isClicked", false);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(cubeTag) && !buttonClicked)
        {
            buttonClicked = true;
            buttonAnimate.SetBool("isClicked", true);
            buttonClickSfx.Play();
            SetButtonColor(Color.green);
        }
    }

    private void SetButtonColor(Color color)
    {
        buttonRenderer.material.color = color;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(cubeTag) && buttonClicked)
        {
            buttonClicked = false;
            buttonAnimate.SetBool("isClicked", false);
            SetButtonColor(Color.red);
        }
    }

}
