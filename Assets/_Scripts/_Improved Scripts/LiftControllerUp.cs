using UnityEngine;

public class LiftControllerUp : MonoBehaviour
{
    [Header("Lift Speed")]
    [SerializeField] private float liftSpeed = 1f;

    [Header("Lift Delay")]
    [SerializeField] private float delayTime = 5f;

    [Header("Dependeny")]
    [SerializeField] private Transform liftTop;

    private Vector3 initialPosition;
    private bool isPlayerInside;
    private bool isLiftingUp;
    private float delayTimer;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            if (isLiftingUp)
            {
                if (transform.position.y < liftTop.position.y)
                {
                    float newY = Mathf.MoveTowards(transform.position.y, liftTop.position.y, liftSpeed * Time.deltaTime);
                    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                }
                else
                {
                    if (delayTimer <= 0f)
                    {
                        isLiftingUp = false;
                        delayTimer = delayTime;
                        AudioActions.onToggleLiftAudioPlay?.Invoke(true);
                    }
                    else
                    {
                        delayTimer -= Time.deltaTime;
                    }
                }
            }
            else
            {
                if (transform.position.y > initialPosition.y)
                {
                    float newY = Mathf.MoveTowards(transform.position.y, initialPosition.y, liftSpeed * Time.deltaTime);
                    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                }
                else
                {
                    AudioActions.onToggleLiftAudioPlay?.Invoke(false);
                }
            }
        }
        else
        {
            if (transform.position.y > initialPosition.y)
            {
                float newY = Mathf.MoveTowards(transform.position.y, initialPosition.y, liftSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            else
            {
                AudioActions.onToggleLiftAudioPlay?.Invoke(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
            isLiftingUp = true;
            delayTimer = delayTime;
            AudioActions.onToggleLiftAudioPlay?.Invoke(true);
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
