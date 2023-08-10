using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class IntroTextTrigger : MonoBehaviour
{
    [Header("IntroText Canvas")]
    [SerializeField] private Canvas introTextCanvas;
    [SerializeField] private GameObject titleImage;
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Delay for Text")]
    [SerializeField] float textDelay;

    private Animator introTextAnim;
    private Animator levelTextAnim;
    private Animator titleImageAnim;

    private void Start()
    {
        titleImageAnim = titleImage.GetComponent<Animator>();
        introTextAnim = introText.GetComponent<Animator>();
        levelTextAnim = levelText.GetComponent<Animator>();

        introTextCanvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            introTextCanvas.enabled = true;
            titleImageAnim.SetBool("isFade", true);
            introTextAnim.SetBool("isFade", true);
            levelTextAnim.SetBool("isFade", true);
            StartCoroutine(DisableTextAfterDelay(textDelay));
        }
    }

    private IEnumerator DisableTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        titleImageAnim.SetBool("isFade", false);
        introTextAnim.SetBool("isFade", false);
        levelTextAnim.SetBool("isFade", false);
        yield return new WaitForSeconds(1f);
        introTextCanvas.enabled = false;
    }
}
