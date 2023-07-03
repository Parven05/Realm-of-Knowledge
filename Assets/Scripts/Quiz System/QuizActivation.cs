using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizActivation : MonoBehaviour
{
    public QuizManager quizManager;
    private bool quizActivated = false;

    private void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();
        quizManager.enabled = false;
        quizManager.openText.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("QuizActivationCube") && !quizActivated)
        {
            quizActivated = true;
            quizManager.enabled = true;
            quizManager.openText.enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("QuizActivationCube") && quizActivated)
        {
            quizActivated = false;
            quizManager.enabled = false;
            quizManager.openText.enabled = false;
        }
    }
}
