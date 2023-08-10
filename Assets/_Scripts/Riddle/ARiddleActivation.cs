using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARiddleActivation : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject; // The game object to enable when all buttons are clicked
    [SerializeField] private RiddleActivation[] buttons; // Array of RiddleActivation scripts attached to the buttons

    private bool allButtonsClicked = false;

    private void Update()
    {
        // Check if all buttons have been clicked
        allButtonsClicked = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].ButtonClicked)
            {
                allButtonsClicked = false;
                break;
            }
        }

        // Set the target game object's visibility based on the button states
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(allButtonsClicked);
        }
    }
}
