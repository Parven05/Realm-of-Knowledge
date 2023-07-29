using UnityEngine;

public class DebugGame : MonoBehaviour
{
    // Array of door objects
    public GameObject[] doors;

    private void Start()
    {
        // Call the function to remove the MeshCollider from each door
        RemoveMeshColliders();
    }

    private void RemoveMeshColliders()
    {
        foreach (GameObject door in doors)
        {
            // Check if the door has a MeshCollider component attached
            BoxCollider boxCollider = door.GetComponent<BoxCollider>();

            if (boxCollider != null)
            {
                // Destroy the MeshCollider component
                Destroy(boxCollider);
                Debug.Log("MeshCollider removed from door: " + door.name);
            }
            else
            {
                Debug.Log("No MeshCollider found on door: " + door.name);
            }
        }
    }
}
