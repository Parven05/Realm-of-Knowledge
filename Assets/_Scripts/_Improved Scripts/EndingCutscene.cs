using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    [Header("Ending Scene")]
    [SerializeField] private GameObject logoImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button mainMenuButton;

    [Header("Delay in ending scene")]
    [SerializeField] private float delayBeforePanelAnimation = 2.0f;
    [SerializeField] private float delayBeforeLogoAnimation = 2.0f;
    [SerializeField] private float delayBeforeLogoFadeAnimation = 2.0f;
    [SerializeField] private float buttonDelay = 2.0f;

    private Animator logoAnim;
    private Animator panelAnim;

    private bool hasPlayed = false;

    private void Start()
    {
        logoAnim = logoImage.GetComponent<Animator>();
        panelAnim = panel.GetComponent<Animator>();

        mainMenuButton.gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        AudioActions.onUiButtonClickAudioPlay?.Invoke();
        SceneManager.LoadScene("Main Menu");
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

        yield return new WaitForSeconds(delayBeforePanelAnimation);

        AudioActions.onEndBgmPlay?.Invoke();
        panelAnim.SetBool("isFade", true);
        AudioActions.onTogglePlayerAudio?.Invoke(false);
        GameActions.onToggleCursorState?.Invoke(false);
        

        yield return new WaitForSeconds(delayBeforeLogoAnimation);

        logoAnim.SetBool("isFade", true);

        yield return new WaitForSeconds(delayBeforeLogoFadeAnimation);

        logoAnim.SetBool("isUp", true);

        yield return new WaitForSeconds(buttonDelay);

        mainMenuButton.gameObject.SetActive(true);
        GameActions.onToggleCursorState?.Invoke(true);

        hasPlayed = true;
    }
}
