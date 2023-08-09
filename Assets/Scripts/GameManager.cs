using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject footstepsSFX;
    [SerializeField] private GameObject jumpSFX;
    [SerializeField] private GameObject crossHair;

    public void DisablePlayerSound ()
    {
        footstepsSFX.SetActive(false);
        jumpSFX.SetActive(false);
        crossHair.SetActive(false);
    }

    public void EnablePlayerSound()
    {
        footstepsSFX.SetActive(true);
        jumpSFX.SetActive(true);
        crossHair.SetActive(true);
    }

    private void OnEnable()
    {
        Actions.onDisablePlayerInteraction += DisablePlayerSound;
        Actions.onEnablePlayerInteraction += EnablePlayerSound;
    }

    private void OnDisable()
    {
        Actions.onDisablePlayerInteraction -= DisablePlayerSound;
        Actions.onEnablePlayerInteraction -= EnablePlayerSound;
    }

}
