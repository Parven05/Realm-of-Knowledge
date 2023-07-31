using UnityEngine;

public class QuizCompletionHandler : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AudioSource completedSfx;
    [SerializeField] private Light buttonLight; // Reference to the light object

    private Renderer buttonRenderer;
    private bool colorChanged = false;
    private bool isCompleted = false;

    public bool IsCompleted => isCompleted;

    public delegate void QuizCompletedDelegate();
    public event QuizCompletedDelegate OnQuizCompleted;

    private void Start()
    {
        buttonRenderer = GetComponent<Renderer>();
    }

    private void SetButtonColor(Color color)
    {
        buttonRenderer.material.color = color;
        // Set the emission color of the button's material (assuming it uses a standard shader)
        buttonRenderer.material.SetColor("_EmissionColor", color); // You can adjust the multiplier (2f) to control the emission brightness

        // Set the color of the light component
        if (buttonLight != null)
        {
            buttonLight.color = color;
        }
    }

    public void QuizCompletion()
    {
        if (!colorChanged)
        {
            completedSfx.Play();
            SetButtonColor(Color.green);
            colorChanged = true;
            isCompleted = true;

            // Raise the OnQuizCompleted event when the quiz is completed
            OnQuizCompleted?.Invoke();
        }
    }
}
