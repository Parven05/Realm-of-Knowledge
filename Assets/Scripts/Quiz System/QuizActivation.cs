using UnityEngine;
using TMPro;

public class QuizActivation : MonoBehaviour
{
    [SerializeField] private Animator buttonAnimate;
    [SerializeField] private AudioSource buttonClickSfx;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TextMeshProUGUI quizText;
    [SerializeField] private GameObject quizScreen;

    private Renderer buttonRenderer;
    private Renderer quizRenderer;
    private bool buttonClicked = false;
    private bool quizActivated = false;

    private void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();
        quizRenderer = quizScreen.GetComponent<Renderer>();
        buttonAnimate = GetComponent<Animator>();
        buttonRenderer = GetComponent<Renderer>();

        if (!buttonClicked && !quizActivated)
        {
            quizActivated = false;
            quizManager.enabled = false;
            quizText.enabled = false;
        }

    }

    private void SetButtonClicked(bool isClicked)
    {
        buttonAnimate.SetBool("isClicked", isClicked);
    }
    private void SetButtonColor(Color color)
    {
        buttonRenderer.material.color = color;
    }

    private void SetScreenColor(Color color)
    {
        quizRenderer.material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CubeOrange") && !buttonClicked && !quizActivated)
        {
            buttonClicked = true;

            buttonClickSfx.Play();
            SetButtonColor(Color.green);
            SetButtonClicked(true);

            quizActivated = true;
            SetScreenColor(Color.white);
            quizManager.enabled = true;
            quizText.enabled = true;
           


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("CubeOrange") && buttonClicked && quizActivated)
        {
            buttonClicked = false;

            SetButtonColor(Color.red);
            SetButtonClicked(false);

            quizActivated = false;
            SetScreenColor(Color.black);
            quizManager.enabled = false;
            quizText.enabled = false;

        }
    }
}
