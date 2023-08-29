using UnityEngine;

public class DoorActivation : MonoBehaviour, ISetColor
{
    [Header("Door")]
    [SerializeField] private GameObject doorObject;

    [Header("Button")]
    [SerializeField] private Animator buttonAnimate;  

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

    public void SetColor(Color color, Renderer renderer)
    {
        renderer.material.color = color;
    }

    private void SetButtonClicked(bool isClicked)
    {
        buttonAnimate.SetBool("isClicked", isClicked);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoorActivationCube") && !doorActivated && !buttonClicked)
        {
            doorActivated = true;
            buttonClicked = true;

            AudioActions.onButtonClickAudioPlay?.Invoke();
            SetColor(Color.green, buttonRenderer);
            SetButtonClicked(true);
            AudioActions.onToggleDoorAudio?.Invoke(true);

            doorAnimate.SetBool("isOpen", true);
        }
    }
}
