using System.Collections;
using UnityEngine;

public class LightOnOff : MonoBehaviour, ISetColor
{
    [Header("Light Flickering")]
    [SerializeField] private GameObject lightsToActivate;
    [SerializeField] private Material emissionMaterials;

    [Header("Flicker Delay")]
    [SerializeField] private float activationDelay = 0.5f;
    [SerializeField] private int flickerCount = 5;
    [SerializeField] private float flickerInterval = 0.2f;

    [Header("Computer Screen On")]
    [SerializeField] private float screenDelay = 0.2f;
    [SerializeField] private GameObject quizScreen;

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
    public void SetColor(Color color, Renderer renderer)
    {
        renderer.material.color = color;
    }

    private void Start()
    {
        SetColor(Color.black, quizRenderer);
        StartCoroutine(ActivateLightsWithDelay());
    }

    private IEnumerator ActivateLightsWithDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        for (int i = 0; i < flickerCount; i++)
        {
            AudioActions.onLightAudioPlay?.Invoke();
            lightsToActivate.SetActive(true);
            emissionMaterials.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(flickerInterval);

            lightsToActivate.SetActive(false);
            emissionMaterials.DisableKeyword("_EMISSION");
            yield return new WaitForSeconds(flickerInterval);
        }


        AudioActions.onLightAudioPlay?.Invoke();
        lightsToActivate.SetActive(true);
        emissionMaterials.EnableKeyword("_EMISSION");

        yield return new WaitForSeconds(screenDelay);
        AudioActions.onScreenAudioPlay?.Invoke();
        SetColor(Color.white, quizRenderer);
    }
}
