using UnityEngine;

public class LiftController : MonoBehaviour
{
    public GameObject liftObject; // Reference to the GameObject with the Animator component

    private Animator liftAnimator;
    private bool isUp = false;

    private void Start()
    {
        if (liftObject != null)
            liftAnimator = liftObject.GetComponent<Animator>();
        else
            Debug.LogError("Lift object is not assigned!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUp)
        {
            liftAnimator.SetBool("isUp", true);
            isUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isUp)
        {
            liftAnimator.SetBool("isUp", false);
            isUp = false;
        }
    }
}
