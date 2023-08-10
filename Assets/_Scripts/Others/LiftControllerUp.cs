using UnityEngine;

public class LiftControllerUp : MonoBehaviour
{
    [Header("Lift Speed")]
    [SerializeField] private float liftSpeed = 1f;

    [Header("Lift Delay")]
    [SerializeField] private float delayTime = 5f;

    [Header("Lift Sound Effect")]
    [SerializeField] private AudioSource liftAudioSource;

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
                        PlayLiftSound();
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
                    StopLiftSound();
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
                StopLiftSound();
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
            PlayLiftSound();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private void PlayLiftSound()
    {
        if (liftAudioSource != null && isLiftingUp)
        {
            liftAudioSource.Play();
        }
    }

    private void StopLiftSound()
    {
        if (liftAudioSource != null)
        {
            liftAudioSource.Stop();
        }
    }
}
