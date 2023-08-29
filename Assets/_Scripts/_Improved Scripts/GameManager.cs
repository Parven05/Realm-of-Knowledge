using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController player;
    [SerializeField] private GameObject crossHair;

    private void Start()
    {
        GameActions.onToggleCursorState?.Invoke(false);
    }
    private void SetCursorState(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;

        crossHair.SetActive(!enabled);
    }

    private void SetPlayerMovementState(bool enabled)
    {
        player.enabled = enabled;
    }

    private void OnEnable()
    {
        GameActions.onToggleCursorState += SetCursorState;
        GameActions.onTogglePlayerMovement += SetPlayerMovementState;
    }

    private void OnDisable()
    {
        GameActions.onToggleCursorState -= SetCursorState;
        GameActions.onTogglePlayerMovement -= SetPlayerMovementState;
    }
}

