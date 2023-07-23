using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCutscene : MonoBehaviour
{
    [SerializeField] private GameObject logoImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private AudioSource endBGM;
    [SerializeField] private float delayBeforePanelAnimation = 2.0f;
    [SerializeField] private float delayBeforeLogoAnimation = 2.0f;
    [SerializeField] private float delayBeforeLogoFadeAnimation = 2.0f;

    [SerializeField] private FootSteps playerFootsteps;
    [SerializeField] private GameObject playerCursor;

    private Animator logoAnim;
    private Animator panelAnim;

    private bool hasTransitioned = false;
    private bool hasPlayed = false;

    private void Start()
    {
        logoAnim = logoImage.GetComponent<Animator>();
        panelAnim = panel.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            StartCoroutine(PlayAnimationWithDelay());
        }
    }

    private IEnumerator PlayAnimationWithDelay()
    {
        hasTransitioned = true;

        yield return new WaitForSeconds(delayBeforePanelAnimation);

        endBGM.Play();
        panelAnim.SetBool("isFade", true);
        playerCursor.SetActive(false);
        playerFootsteps.enabled = false;

        yield return new WaitForSeconds(delayBeforeLogoAnimation);

        logoAnim.SetBool("isFade", true);

        yield return new WaitForSeconds(delayBeforeLogoFadeAnimation);

        logoAnim.SetBool("isUp", true);

        hasPlayed = true;
    }
}
