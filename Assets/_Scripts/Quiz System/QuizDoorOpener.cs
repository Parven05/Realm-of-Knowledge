using UnityEngine;


public class QuizDoorOpener : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private GameObject doorTrigger;
    
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
            AudioActions.onToggleDoorAudio?.Invoke(true);
        }
    }
}
