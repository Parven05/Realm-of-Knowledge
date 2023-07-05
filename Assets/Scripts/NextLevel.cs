using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private float LoadDelay;

    private void Update()
    {
        if (quizManager.TotalQuizCompleted() == true)
        {
            StartCoroutine(LoadNextSceneAfterDelay(LoadDelay));
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
