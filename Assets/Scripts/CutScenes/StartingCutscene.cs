using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCutscene : MonoBehaviour
{
    [Header("Intro Scene")]
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private GameObject quoteText;

    [Header("Delay in Intro Scene")]
    [SerializeField] private float panelDelay = 1f;
    [SerializeField] private int loadingLoopCount = 3;
    [SerializeField] private float loadingDelay = 1.5f;
    [SerializeField] private float quoteDelay = 2.0f;

    [Header("Player")]
    [SerializeField] private FootSteps playerFootsteps;
    [SerializeField] private FirstPersonController playerMovement;
    [SerializeField] private GameObject playerCursor;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource lightHumSFX;


    private Animator panelAnim;
    private Animator loadingAnim;
    private Animator quoteAnim;

    private void Awake()
    {
        panelAnim = panel.GetComponent<Animator>();
        loadingAnim = loadingText.GetComponent<Animator>();
        quoteAnim = quoteText.GetComponent<Animator>();
    }

    private void Start()
    {
        playerFootsteps.enabled = false;
        playerMovement.enabled = false;
        playerCursor.SetActive(false);
        StartCoroutine(PlayAnimations());

    }

    private IEnumerator PlayAnimations()
    {
      
        yield return new WaitForSeconds(loadingDelay);
        for (int i = 0; i < loadingLoopCount; i++)
        {
            loadingAnim.SetBool("isFade", true);
            yield return new WaitForSeconds(loadingAnim.GetCurrentAnimatorStateInfo(0).length);
            loadingAnim.SetBool("isFade", false);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(quoteDelay);
        quoteAnim.SetBool("isFade", true);

        yield return new WaitForSeconds(panelDelay);
        panelAnim.SetBool("isFade", true);

        lightHumSFX.Play();
        playerFootsteps.enabled = true;
        playerMovement.enabled = true;
        playerCursor.SetActive(true);
    }
}
