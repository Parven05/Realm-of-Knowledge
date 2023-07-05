using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioSource footStepsSfx;
    [SerializeField] private AudioSource jumpSfx;

    private bool isMoving = false;
    private bool isJumping = false;
    private bool isGrounded = true;

    private void Update()
    {
        bool isMovementKeyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isJumpKeyPressed = Input.GetKey(KeyCode.Space);

        if (isMovementKeyPressed)
        {
            if (!isMoving)
            {
                footStepsSfx.enabled = true;
                isMoving = true;
            }
        }
        else
        {
            footStepsSfx.enabled = false;
            isMoving = false;
        }

        if (isJumpKeyPressed && isGrounded && !isJumping)
        {
            jumpSfx.Play();
            isJumping = true;
            isGrounded = false;
        }

        if (!isJumpKeyPressed)
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
