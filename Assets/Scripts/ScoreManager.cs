using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }

    public event System.Action<int> OnScoreUpdated;

    [Header("Score Canvas")]
    [SerializeField] private Canvas scoreCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private int score = 0;
    private int roomScoreLimit = 10;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

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
    public void ChangeScoreText(string text)
    {
       scoreText.text = text;
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
