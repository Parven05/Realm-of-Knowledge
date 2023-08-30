using UnityEngine;

public class QuizCompletionHandler : MonoBehaviour, ISetColor
{
    [SerializeField] private ScoreManager scoreManager;
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

    public void SetColor(Color color, Renderer renderer)
    {
        renderer.material.color = color;

        renderer.material.SetColor("_EmissionColor", color);

        if (buttonLight != null)
        {
            buttonLight.color = color;
        }
    }

    public void QuizCompletion()
    {
        if (!colorChanged)
        {
            AudioActions.onCompletedAudioPlay?.Invoke();
            SetColor(Color.green, buttonRenderer);
            colorChanged = true;
            isCompleted = true;

            OnQuizCompleted?.Invoke();
        }
    }
}
