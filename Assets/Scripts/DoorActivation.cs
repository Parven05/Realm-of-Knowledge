using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    [SerializeField] private GameObject doorObject;
    [SerializeField] private Animator buttonAnimate;
    [SerializeField] private AudioSource doorOpenSfx;
    [SerializeField] private AudioSource doorCloseSfx;
    [SerializeField] private AudioSource buttonClickSfx;

    private Animator doorAnimate;
    private Renderer buttonRenderer;
    private bool doorActivated = false;
    private bool buttonClicked = false;

    private void Start()
    {
        buttonAnimate = GetComponent<Animator>();
        doorAnimate = doorObject.GetComponent<Animator>();
        buttonRenderer = GetComponent<Renderer>();
    }

    private void SetButtonClicked(bool isClicked)
    {
        buttonAnimate.SetBool("isClicked", isClicked);
    }
    private void SetButtonColor(Color color)
    {
        buttonRenderer.material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoorActivationCube") && !doorActivated && !buttonClicked)
        {
            doorActivated = true;
            buttonClicked = true;

            buttonClickSfx.Play();
            SetButtonColor(Color.green);
            SetButtonClicked(true);
            doorOpenSfx.Play();

            doorAnimate.SetBool("isOpen", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoorActivationCube") && doorActivated && buttonClicked)
        {
            doorActivated = false;
            buttonClicked = false;

            SetButtonColor(Color.red);
            SetButtonClicked(false);
            doorCloseSfx.Play();

            doorAnimate.SetBool("isOpen", false);
        }
    }
}
