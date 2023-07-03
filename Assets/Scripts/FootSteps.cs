using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioSource footStepsSfx;
    [SerializeField] private AudioSource jumpSfx;

    private bool isMoving = false;
    private bool isJumping = false;

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

        if (isJumpKeyPressed)
        {
            if (!isJumping)
            {
                jumpSfx.Play();
                isJumping = true;
            }
        }
        else
        {
            isJumping = false;
        }
    }
}
