using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public KeyCode inventoryKey = KeyCode.I;

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
    }
}
