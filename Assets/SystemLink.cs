using UnityEngine;
using UnityEngine.EventSystems;

public class SystemLink : MonoBehaviour, IPointerClickHandler
{
    [Header("Links")]
    [SerializeField] private bool enableGitHubLink = false;
    [SerializeField] private bool enableYoutubeLink = false;
    [SerializeField] private bool enablePatreonLink = false;

    [SerializeField] private string gitHubLink = "https://github.com/YourGitHubUsername/YourRepositoryName";
    [SerializeField] private string youtubeLink = "https://youtube/YourChannelLink";
    [SerializeField] private string patreonLink = "https://www.patreon.com/ProudBananaEntertainment";

    public void OpenGithub()
    {
        Application.OpenURL(gitHubLink);
    }

    public void OpenYoutube()
    {
        Application.OpenURL(youtubeLink);
    }

    public void OpenPatreonLink()
    {
        Application.OpenURL(patreonLink);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (enableGitHubLink)
                OpenGithub();

            if (enableYoutubeLink)
                OpenYoutube();

            if (enablePatreonLink)
                OpenPatreonLink();
        }
    }
}
