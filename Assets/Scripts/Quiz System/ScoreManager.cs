using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public event System.Action<int> OnScoreUpdated;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] Canvas scoreCanvas;
    private int score = 0;
    private int roomScoreLimit = 10; // Score limit per room

    public void AddScore(int points)
    {
        if (score >= roomScoreLimit)
        {
            return; // Score limit reached, no additional score can be added
        }

        score += points;
        if (score > roomScoreLimit)
        {
            score = roomScoreLimit; // Cap the score to the room limit
        }

        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public int GetRoomScoreLimit()
    {
        return roomScoreLimit;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        OnScoreUpdated?.Invoke(GetCurrentScore());
    }

    public void EnableScoreCanvas()
    {
        scoreCanvas.enabled = true;
    }

    public void DisableScoreCanvas()
    {
        scoreCanvas.enabled = false;
    }
}
