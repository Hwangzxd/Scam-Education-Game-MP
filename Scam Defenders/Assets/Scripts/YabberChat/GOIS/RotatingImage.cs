using UnityEngine;

public class RotatingImage : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation in degrees per second

    private RectTransform rectTransform;

    private void Start()
    {
        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Rotate the RectTransform continuously
        rectTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
