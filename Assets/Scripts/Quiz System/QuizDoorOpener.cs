using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizDoorOpener : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private GameObject doorTrigger;
    [SerializeField] private AudioSource openDoorSFX;
    
    private Animator doorAnimator;

    private void Start()
    {
        doorTrigger.SetActive(false);
        doorAnimator = doorObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (quizManager.TotalQuizCompleted() == true)
        {
            doorAnimator.SetBool("isOpen", true);
            doorTrigger.SetActive(true);
            openDoorSFX.Play();
        }
    }
}
