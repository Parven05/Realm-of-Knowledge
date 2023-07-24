using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    [Header("Ending Scene")]
    [SerializeField] private GameObject logoImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button mainMenuButton;

    [Header("Ending BGM & Sound Effect")]
    [SerializeField] private AudioSource endBGM;
    [SerializeField] private AudioSource buttonClick;

    [Header("Delay in ending scene")]
    [SerializeField] private float delayBeforePanelAnimation = 2.0f;
    [SerializeField] private float delayBeforeLogoAnimation = 2.0f;
    [SerializeField] private float delayBeforeLogoFadeAnimation = 2.0f;
    [SerializeField] private float buttonDelay = 2.0f;

    [Header("Player")]
    [SerializeField] private GameObject footstepsSFX;
    [SerializeField] private GameObject jumpSFX;
    [SerializeField] private GameObject playerCursor;

    private Animator logoAnim;
    private Animator panelAnim;

    private bool hasTransitioned = false;
    private bool hasPlayed = false;

    private void Start()
    {
        logoAnim = logoImage.GetComponent<Animator>();
        panelAnim = panel.GetComponent<Animator>();

        mainMenuButton.onClick.AddListener(BackToMainMenu);
        mainMenuButton.gameObject.SetActive(false);
    }

    void BackToMainMenu()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Main Menu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            StartCoroutine(PlayAnimationWithDelay());
        }
    }

    private void SetCursorState(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void PlayerInteraction(bool enabled)
    {
        playerCursor.SetActive(enabled);
        footstepsSFX.SetActive(enabled);
        jumpSFX.SetActive(enabled);
    }

    private IEnumerator PlayAnimationWithDelay()
    {
        hasTransitioned = true;

        yield return new WaitForSeconds(delayBeforePanelAnimation);

        endBGM.Play();
        panelAnim.SetBool("isFade", true);
        PlayerInteraction(false);
        

        yield return new WaitForSeconds(delayBeforeLogoAnimation);

        logoAnim.SetBool("isFade", true);

        yield return new WaitForSeconds(delayBeforeLogoFadeAnimation);

        logoAnim.SetBool("isUp", true);

        yield return new WaitForSeconds(buttonDelay);

        mainMenuButton.gameObject.SetActive(true);
        SetCursorState(true);

        hasPlayed = true;
    }
}
