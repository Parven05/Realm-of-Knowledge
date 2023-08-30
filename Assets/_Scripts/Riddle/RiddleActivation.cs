using UnityEngine;

public class RiddleActivation : MonoBehaviour, ISetColor
{
    [Header("Button")]
    [SerializeField] private Animator buttonAnimate;

    [Header("Required Cube")]
    [SerializeField] private CubeTags cubeTag;

    private Renderer buttonRenderer;
    private bool buttonClicked = false;

    private void Start()
    {
        buttonRenderer = GetComponent<Renderer>();
        buttonAnimate = GetComponent<Animator>();
        buttonAnimate.SetBool("isClicked", false);
        
    }
    public bool ButtonClicked
    {
        get { return buttonClicked; }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(cubeTag.ToString()) && !buttonClicked)
        {
            buttonClicked = true;
            buttonAnimate.SetBool("isClicked", true);
            AudioActions.onButtonClickAudioPlay?.Invoke();
            SetColor(Color.green, buttonRenderer);
        }
    }

    public void SetColor(Color color, Renderer renderer)
    {
        renderer.material.color = color;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(cubeTag.ToString()) && buttonClicked)
        {
            buttonClicked = false;
            buttonAnimate.SetBool("isClicked", false);
            SetColor(Color.red, buttonRenderer);
        }
    }

}
