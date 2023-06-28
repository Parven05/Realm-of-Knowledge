using UnityEngine;

public class LiftController : MonoBehaviour
{
    public float liftSpeed = 1f; // Speed at which the lift moves
    public Transform liftTop; // The transform representing the highest position the lift can reach

    private Vector3 initialPosition; // The initial position of the lift
    private bool isPlayerInside; // Flag to track if the player is inside the lift

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            if (transform.position.y < liftTop.position.y)
            {
                float newY = Mathf.Lerp(transform.position.y, liftTop.position.y, liftSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            else if (transform.position.y > liftTop.position.y)
            {
                float newY = Mathf.Lerp(transform.position.y, initialPosition.y, liftSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
        else
        {
            if (transform.position.y > initialPosition.y)
            {
                float newY = Mathf.Lerp(transform.position.y, initialPosition.y, liftSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
