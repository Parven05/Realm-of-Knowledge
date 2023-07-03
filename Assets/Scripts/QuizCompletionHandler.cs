using UnityEngine;

public class QuizCompletionHandler : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AudioSource completedSfx;

    private Renderer buttonRenderer;

    private bool colorChanged = false;

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
        if (currentScore >= 5 && !colorChanged)
        {
            completedSfx.Play();
            SetButtonColor(Color.green);
            colorChanged = true;
        }
    }
}
