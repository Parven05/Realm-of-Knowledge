using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class QuizManager : MonoBehaviour
{
    [Header("Quiz Canvas")]
    [SerializeField] private GameObject quizCanvas;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private Question[] questions;
    [SerializeField] private AudioSource buttonClickedSFX;

    [Header("Result Canvas")]
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button exitButton;

    [Header("Dependencies")]
    [SerializeField] private FirstPersonController player;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject footstepsSFX;
    [SerializeField] private GameObject jumpSFX;

    [SerializeField] private QuizCompletionHandler quizCompletionHandlers;
    [SerializeField] private bool quizCompletion;

    private List<Question> questionPool = new List<Question>();
    private Queue<Question> currentQuestionSet = new Queue<Question>();
    private bool quizTriggered = false;
    private bool quizCompleted = false;
    private int correctAnswerCount = 0;
    private bool quizButtonActivated = false;

    private void Start()
    {
        ScoreManager.instance.DisableScoreCanvas();
        quizCanvas.SetActive(false);
        resultCanvas.SetActive(false);
        SetCursorState(false);
        exitButton.onClick.AddListener(ExitQuiz);
        tryAgainButton.onClick.AddListener(TryAgain);
        InitializeQuestionPool();
    }

    void PlayerInteraction(bool enabled)
    {
        footstepsSFX.SetActive(enabled);
        jumpSFX.SetActive(enabled);
    }


    private void SetCursorState(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && quizButtonActivated)
        {
            quizTriggered = true;
            SetCursorState(false);
            ScoreManager.instance.ResetScore();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            quizTriggered = false;
            quizCanvas.SetActive(false);
            resultCanvas.SetActive(false);
            SetCursorState(false);
        }
    }

    private void Update()
    {
        if (quizTriggered && !quizCompleted && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInteraction(false);
            crossHair.SetActive(false);
            SetCursorState(true);
            Time.timeScale = 0f;
            player.cameraCanMove = false;
            quizCanvas.SetActive(true);
            StartQuiz();
            ScoreManager.instance.EnableScoreCanvas();
        }
    }

    private void StartQuiz()
    {
        if (currentQuestionSet.Count == 0)
        {
            ResetQuiz();
            GenerateQuestionSet();
        }

        DisplayQuestion();
    }

    public void AnswerButtonClicked(int buttonIndex)
    {
        //Play Sound
        buttonClickedSFX.Play();
        Question currentQuestion = currentQuestionSet.Peek();
        if (buttonIndex == currentQuestion.correctAnswerIndex)
        {
            ScoreManager.instance.AddScore(currentQuestion.scorePoints);
            correctAnswerCount++;
            if (correctAnswerCount == 5)
            {
                QuizCompleted();
                return;
            }
        }

        currentQuestionSet.Dequeue();

        if (currentQuestionSet.Count > 0)
        {
            DisplayQuestion();
        }
        else
        {
            QuizCompleted();
        }
    }

    public bool TotalQuizCompleted()
    {
        return quizCompleted;
    }

    private void QuizCompleted()
    {
        crossHair.SetActive(false);
        Time.timeScale = 0f;
        quizCompleted = true;
        player.cameraCanMove = false;
        quizCanvas.SetActive(false);
        resultCanvas.SetActive(true);
        ScoreManager.instance.EnableScoreCanvas();

        string resultMessage = $"{correctAnswerCount} out of 5 answers are correct!";
        resultText.text = resultMessage;

        int currentScore = ScoreManager.instance.GetCurrentScore();
        ScoreManager.instance.ChangeScoreText("Score: " + currentScore);

        if (currentScore == ScoreManager.instance.GetRoomScoreLimit() && quizCompletion)
        {
            if (quizCompletionHandlers != null) 
            {
                quizCompletionHandlers.QuizCompletion();
            }
        }
    }

    private void ExitQuiz()
    {
        buttonClickedSFX.Play();
        PlayerInteraction(true);
        crossHair.SetActive(true);
        SetCursorState(false);
        quizCompleted = false;
        Time.timeScale = 1f;
        player.cameraCanMove = true;
        quizCanvas.SetActive(false);
        resultCanvas.SetActive(false);
        ResetQuiz();
        ScoreManager.instance.DisableScoreCanvas();
        ScoreManager.instance.ResetScore();
    }

    private void TryAgain()
    {
        buttonClickedSFX.Play();
        crossHair.SetActive(false);
        SetCursorState(true);
        Time.timeScale = 0f;
        player.cameraCanMove = false;
        quizCanvas.SetActive(true);
        resultCanvas.SetActive(false);
        ScoreManager.instance.ResetScore();
        correctAnswerCount = 0;
        quizCompleted = false;
        ResetQuiz(); // Reset the quiz before starting it again
        StartQuiz();
        ScoreManager.instance.EnableScoreCanvas();
    }

    private void InitializeQuestionPool()
    {
        questionPool.AddRange(questions);
    }

    private void GenerateQuestionSet()
    {
        ShuffleQuestions(questionPool);
        foreach (var question in questionPool.GetRange(0, 5))
        {
            currentQuestionSet.Enqueue(question);
        }
    }

    private void DisplayQuestion()
    {
        Question currentQuestion = currentQuestionSet.Peek();
        questionText.text = currentQuestion.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button answerButton = answerButtons[i];
            answerButton.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];

            int index = i;
            answerButton.onClick.RemoveAllListeners();
            answerButton.onClick.AddListener(() => AnswerButtonClicked(index));
        }
    }

 

    private void ShuffleQuestions(List<Question> questionList)
    {
        int n = questionList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Question value = questionList[k];
            questionList[k] = questionList[n];
            questionList[n] = value;
        }
    }

    private void ResetQuiz()
    {
        correctAnswerCount = 0;
        currentQuestionSet.Clear();
    }

    public void ActivateQuizButton()
    {
        quizButtonActivated = true;
    }
}

[System.Serializable]
public class Question
{
    public string question;
    public string[] answers;
    public int correctAnswerIndex;
    public int scorePoints;
}
