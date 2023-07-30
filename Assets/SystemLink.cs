using UnityEngine;
using UnityEngine.EventSystems;

public class SystemLink : MonoBehaviour, IPointerClickHandler
{
    // Customize this variable with your GitHub link
    [SerializeField] private string gitHubLink = "https://github.com/YourGitHubUsername/YourRepositoryName";

    // Function to open the GitHub link in the default web browser
    public void OpenLinkInBrowser()
    {
        Application.OpenURL(gitHubLink);
    }

    // Implement the OnPointerClick method from IPointerClickHandler
   public  void OnPointerClick(PointerEventData eventData)
    {
        // Check if the left mouse button was clicked
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Open the GitHub link in the default web browser
            OpenLinkInBrowser();
        }
    }
}
