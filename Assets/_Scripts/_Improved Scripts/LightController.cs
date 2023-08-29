using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    [Header("Light Activation")]
    [SerializeField] private GameObject[] lightsToActivate;
    [SerializeField] private Material[] emissionMaterials; 

    [Header("Light Activation Delay")]
    [SerializeField] private float activationDelay = 0.5f;

    private bool hasInteracted = false;

    private void Awake()
    {
        foreach (Material emissionMaterial in emissionMaterials)
        {
            if (emissionMaterial != null)
            {
                emissionMaterial.DisableKeyword("_EMISSION");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            StartCoroutine(ActivateLightsWithDelay());
            hasInteracted = true;
        }
    }

    private IEnumerator ActivateLightsWithDelay()
    {
        for (int i = 0; i < lightsToActivate.Length; i++)
        {
            yield return new WaitForSeconds(activationDelay);
            AudioActions.onLightAudioPlay?.Invoke();
            lightsToActivate[i].SetActive(true);

            if (i < emissionMaterials.Length && emissionMaterials[i] != null)
            {
                emissionMaterials[i].EnableKeyword("_EMISSION");
            }
        }
    }
}
