using System.Collections;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    [SerializeField] private GameObject lightsToActivate;
    [SerializeField] private Material emissionMaterials;
    [SerializeField] private AudioSource lightOnSfx;
    [SerializeField] private float activationDelay = 0.5f;
    [SerializeField] private int flickerCount = 5; // Number of flickers
    [SerializeField] private float flickerInterval = 0.2f;
    [SerializeField] private float screenDelay = 0.2f;
    [SerializeField] private GameObject quizScreen;
    [SerializeField] private AudioSource screenOnSfx;

    private Renderer quizRenderer;

    private void Awake()
    {
        quizRenderer = quizScreen.GetComponent<Renderer>();
        if (emissionMaterials != null)
        {
            emissionMaterials.DisableKeyword("_EMISSION");
        }
        lightsToActivate.SetActive(false);
    }
    private void SetScreenColor(Color color)
    {
        quizRenderer.material.color = color;
    }

    private void Start()
    {
        SetScreenColor(Color.black);
        StartCoroutine(ActivateLightsWithDelay());
    }

    private IEnumerator ActivateLightsWithDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        for (int i = 0; i < flickerCount; i++)
        {
            lightOnSfx.Play();
            lightsToActivate.SetActive(true);
            emissionMaterials.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(flickerInterval);

            lightsToActivate.SetActive(false);
            emissionMaterials.DisableKeyword("_EMISSION");
            yield return new WaitForSeconds(flickerInterval);
        }

        
        lightOnSfx.Play();
        lightsToActivate.SetActive(true);
        emissionMaterials.EnableKeyword("_EMISSION");

        yield return new WaitForSeconds(screenDelay);
        screenOnSfx.Play();
        SetScreenColor(Color.white);
    }
}
