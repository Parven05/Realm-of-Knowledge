using UnityEngine;

public class QuizCompletionHandler : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AudioSource completedSfx;
    [SerializeField] private int targetScore;

    private Renderer buttonRenderer;
    private bool colorChanged = false;
    private bool isCompleted = false;

    public bool IsCompleted => isCompleted;

    public delegate void QuizCompletedDelegate();
    public event QuizCompletedDelegate OnQuizCompleted;

    private void Start()
    {
        buttonRenderer = GetComponent<Renderer>();
        scoreManager.OnScoreUpdated += HandleScoreUpdated;
    }

    private void SetButtonColor(Color color)
    {
        buttonRenderer.material.color = color;
    }

    private void OnDestroy()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreUpdated -= HandleScoreUpdated;
        }
    }

    private void HandleScoreUpdated(int currentScore)
    {
        if (currentScore >= targetScore && !colorChanged && gameObject.CompareTag("Room1"))
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
