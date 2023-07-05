using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroTextTrigger : MonoBehaviour
{
    [SerializeField] Canvas introTextCanvas;
    [SerializeField] float textDelay;
    [SerializeField] TextMeshProUGUI introText;

    private Animator introTextAnim;

    private void Start()
    {
        introTextAnim = introText.GetComponent<Animator>();
        introTextCanvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            introTextCanvas.enabled = true;
            introTextAnim.SetBool("isFade", true);
            StartCoroutine(DisableTextAfterDelay(textDelay));
        }
    }

    private IEnumerator DisableTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        introTextAnim.SetBool("isFade", false);
        yield return new WaitForSeconds(1f); // Delay to allow the fade-out animation to complete
        introTextCanvas.enabled = false;
    }
}
