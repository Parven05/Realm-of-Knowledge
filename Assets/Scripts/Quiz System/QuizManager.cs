using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public GameObject quizCanvas;
    public GameObject resultCanvas;
    public Text questionText;
    public Button tryAgainButton;
    public Button exitButton;
    public Button[] answerButtons;
    public Text resultText;
    public Text scoreText;
    public TextMeshProUGUI openText;

    public Question[] questions;
    public ScoreManager scoreManager;
    public FirstPersonController firstPersonController;
    public ToggleInventory inventory;
    public Animator animator;

    private List<Question> questionPool = new List<Question>();
    private Queue<Question> currentQuestionSet = new Queue<Question>();
    private bool quizTriggered = false;
    private bool quizCompleted = false;
    private int correctAnswerCount = 0;


    private void Start()
    {
        quizCanvas.SetActive(false);
        resultCanvas.SetActive(false);
        SetCursorState(false);
        exitButton.onClick.AddListener(ExitQuiz);
        tryAgainButton.onClick.AddListener(TryAgain);
        animator = FindAnyObjectByType<Animator>();
        InitializeQuestionPool();
    }

    private void SetCursorState(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            quizTriggered = true;
            SetCursorState(false);
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
            SetCursorState(true);
            Time.timeScale = 0f;
            firstPersonController.cameraCanMove = false;
            quizCanvas.SetActive(true);
            StartQuiz();
            inventory.enabled = false;

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
        Question currentQuestion = currentQuestionSet.Peek();
        if (buttonIndex == currentQuestion.correctAnswerIndex)
        {
            scoreManager.AddScore(currentQuestion.scorePoints);
            Debug.Log("Correct Answer!");
           

            correctAnswerCount++;
            if (correctAnswerCount == 5)
            {
                QuizCompleted();
                return;
            }
        }
        else
        {
            Debug.Log("Wrong Answer!");
         
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


    private void QuizCompleted()
    {
        Debug.Log("Quiz completed! Score: " + scoreManager.GetCurrentScore());
        openText.gameObject.SetActive(false);
        Time.timeScale = 0f;
        quizCompleted = true;
        firstPersonController.cameraCanMove = false;
        quizCanvas.SetActive(false);
        resultCanvas.SetActive(true);

        string resultMessage = $"{correctAnswerCount} out of 5 answers are correct!";
        resultText.text = resultMessage;

        int currentScore = scoreManager.GetCurrentScore();
        scoreText.text = "Score: " + currentScore;

        animator.SetBool("isOpen", true);

    }

    private void ExitQuiz()
    {
        SetCursorState(false);
        quizCompleted = false;
        Time.timeScale = 1f;
        firstPersonController.cameraCanMove = true;
        quizCanvas.SetActive(false);
        resultCanvas.SetActive(false);
        inventory.enabled = true;
    }

    private void TryAgain()
    {
        SetCursorState(true);
        Time.timeScale = 0f;
        firstPersonController.cameraCanMove = false;
        quizCanvas.SetActive(true);
        resultCanvas.SetActive(false);
        StartQuiz();
        scoreManager.ResetScore();
    }

    private void InitializeQuestionPool()
    {
        questionPool = questions.ToList();
    }

    private void GenerateQuestionSet()
    {
        ShuffleQuestions(questionPool);
        foreach (var question in questionPool.Take(5))
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
            answerButton.GetComponentInChildren<Text>().text = currentQuestion.answers[i];

            int index = i;
            answerButton.onClick.RemoveAllListeners();
            answerButton.onClick.AddListener(() => AnswerButtonClicked(index));

        }
    }

    private void ResetQuiz()
    {
        correctAnswerCount = 0;
        currentQuestionSet.Clear();
    }

    private void ShuffleQuestions(List<Question> questionList)
    {
        for (int i = 0; i < questionList.Count - 1; i++)
        {
            int randomIndex = Random.Range(i, questionList.Count);
            Question temp = questionList[randomIndex];
            questionList[randomIndex] = questionList[i];
            questionList[i] = temp;
        }
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