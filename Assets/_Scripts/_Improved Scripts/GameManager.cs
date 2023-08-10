using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    private void OnEnable()
    {
        GameActions.onToggleCursorState += SetCursorState;
    }

    private void OnDisable()
    {
        GameActions.onToggleCursorState -= SetCursorState;
    }
}

