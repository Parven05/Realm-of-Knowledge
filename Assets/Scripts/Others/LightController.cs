using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private GameObject[] lightsToActivate;
    [SerializeField] private AudioSource lightOnSfx;
    [SerializeField] private float activationDelay = 0.5f;

    private bool hasInteracted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            StartCoroutine(ActivateLightsWithDelay());
            hasInteracted = true;
        }
    }

    private System.Collections.IEnumerator ActivateLightsWithDelay()
    {
        foreach (GameObject lightObject in lightsToActivate)
        {
            yield return new WaitForSeconds(activationDelay);
            lightOnSfx.Play();
            lightObject.SetActive(true);
        }
    }
}
