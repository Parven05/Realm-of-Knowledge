using UnityEngine;
using TMPro;

public class QuizActivation : MonoBehaviour, ISetColor
{
    [Header("Computer Screen On")]
    [SerializeField] private GameObject quizScreen;
   
    [Header("Enter e Text")]
    [SerializeField] private TextMeshProUGUI quizText;

    [Header("Required Cube")] 
    CubeTags currentCubeTag;

    private Animator buttonAnimate;
    private Renderer buttonRenderer;
    private Renderer screenRenderer;

    private bool buttonClicked = false;
    private bool quizActivated = false;

    private void Start()
    {
        currentCubeTag = CubeTags.CubeUnknown;
        quizText.enabled = false;

        screenRenderer = quizScreen.GetComponent<Renderer>();
        buttonAnimate = GetComponent<Animator>();
        buttonRenderer = GetComponent<Renderer>();

        SetButtonClicked(false);
    }

    private void SetButtonClicked(bool isClicked)
    {
        buttonAnimate.SetBool("isClicked", isClicked);
    }

    public void SetColor(Color color, Renderer renderer)
    {
        renderer.material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(currentCubeTag.ToString()) && !buttonClicked && !quizActivated)
        {
            buttonClicked = true;

            AudioActions.onButtonClickAudioPlay?.Invoke();
            SetColor(Color.green, buttonRenderer);
            SetButtonClicked(true);

            quizActivated = true;
            AudioActions.onScreenAudioPlay?.Invoke();
            SetColor(Color.white, screenRenderer);
            QuizManager.onToggleQuiz?.Invoke(true);
            quizText.enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(currentCubeTag.ToString()) && buttonClicked && quizActivated)
        {
            buttonClicked = false;

            SetColor(Color.red, buttonRenderer);
            SetButtonClicked(false);

            quizActivated = false;
            SetColor(Color.black, screenRenderer);
            QuizManager.onToggleQuiz?.Invoke(false);
            quizText.enabled = false;
        }
    }
}
