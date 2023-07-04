using UnityEngine;
using TMPro;

public class QuizActivation : MonoBehaviour
{
    [SerializeField] private Animator buttonAnimate;
    [SerializeField] private AudioSource buttonClickSfx;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TextMeshProUGUI quizText;
    [SerializeField] private GameObject quizScreen;
    [SerializeField] private AudioSource screenOnSfx;
    [SerializeField] private string cubeTag; // New serialized field for the cube tag

    private Renderer buttonRenderer;
    private Renderer quizRenderer;
    private bool buttonClicked = false;
    private bool quizActivated = false;

    private void Start()
    {
        quizManager.enabled = false;
        quizText.enabled = false;

        quizRenderer = quizScreen.GetComponent<Renderer>();
        buttonAnimate = GetComponent<Animator>();
        buttonRenderer = GetComponent<Renderer>();

        SetButtonColor(Color.red);
        SetButtonClicked(false);
        SetScreenColor(Color.black);
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
        if (collision.gameObject.CompareTag(cubeTag) && !buttonClicked && !quizActivated)
        {
            OnButtonClick();
            buttonClicked = true;

            buttonClickSfx.Play();
            SetButtonColor(Color.green);
            SetButtonClicked(true);

            quizActivated = true;
            screenOnSfx.Play();
            SetScreenColor(Color.white);
            quizManager.enabled = true;
            quizText.enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(cubeTag) && buttonClicked && quizActivated)
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

    private void OnButtonClick()
    {
        quizManager.ActivateQuizButton();
    }
}
