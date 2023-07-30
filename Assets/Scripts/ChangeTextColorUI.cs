using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ChangeTextColorUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;
    private Color originalColor;
    private Color hoverColor = Color.white;

    private void Start()
    {
        // Get the Text component of the button
        buttonText = GetComponent<TextMeshProUGUI>();

        // Store the original color of the button
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // When the pointer hovers over the button, change the text color to white
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // When the pointer exits the button, revert the text color to the original color
        buttonText.color = originalColor;
    }
}
