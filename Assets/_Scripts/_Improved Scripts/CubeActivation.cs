using UnityEngine;

public class CubeActivation : MonoBehaviour, ISetColor
{
    [Header("Required Cube")]
    [SerializeField] private GameObject cubeObject;
    [SerializeField] private CubeTags newCubeTag;
    [SerializeField] private Color newColor;

    private Renderer cubeRenderer;
    private bool cubeActivated = false;

    private void Start()
    {
        cubeRenderer = cubeObject.GetComponent<Renderer>();
    }

    public void SetColor(Color color, Renderer renderer)
    {
        renderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cubeActivated)
        {
            cubeActivated = true;
            cubeObject.tag = newCubeTag.ToString();
            SetColor(newColor, cubeRenderer);
        }
    }
}
