using UnityEngine;

public class FallEffect : MonoBehaviour
{
    private bool isFalling = false;
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player") && !isFalling)
        {
            AudioActions.onToggleFallAudio.Invoke(true);
            isFalling = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isFalling)
        {
            AudioActions.onToggleFallAudio.Invoke(false);
            isFalling = false;
        }
    }
}
