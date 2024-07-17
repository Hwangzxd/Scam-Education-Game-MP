using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    Collider2D collider2d;
    public string destinationTag = "DropArea";
    public float snapDistance = 1.0f; // Adjust this distance as needed
    public float releaseDistance = 2.0f; // Distance to release from drop area

    public Color defaultColor = Color.white;
    public Color snapColor = Color.red; // Color when item is snapped

    private Camera mainCamera;
    private Transform snapTransform; // Store the transform of the closest drop area
    private bool isSnapped = false; // Flag to track if the item is snapped to an area
    public SpriteRenderer spriteRenderer; // To change the color of the sprite

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.color = defaultColor; // Set the initial color
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        snapTransform = null; // Reset snap transform on mouse down
        isSnapped = false; // Reset snapped flag
    }

    void OnMouseDrag()
    {
        Vector3 newPos = MouseWorldPosition() + offset;
        newPos = ClampToViewport(newPos);
        transform.position = newPos;

        // Check for nearby drop areas within snapDistance
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, snapDistance);
        bool nearDropArea = false; // Flag to track proximity to a drop area

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag(destinationTag))
            {
                // Calculate distance between center points of item and drop area
                float distance = Vector3.Distance(transform.position, col.transform.position);

                // Snap only if close enough (within snapDistance)
                if (distance <= snapDistance)
                {
                    snapTransform = col.transform;
                    isSnapped = true; // Set snapped flag
                    nearDropArea = true; // Indicate proximity to drop area
                    break;
                }
                else
                {
                    snapTransform = null;
                    isSnapped = false; // Reset snapped flag if too far
                }
            }
        }

        // Change color if isSnapped is true
        if (isSnapped)
        {
            spriteRenderer.color = snapColor;
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }

        // Release snap if dragged too far from any drop area
        if (snapTransform != null)
        {
            float distanceToSnap = Vector3.Distance(transform.position, snapTransform.position);
            if (distanceToSnap > releaseDistance)
            {
                snapTransform = null;
                isSnapped = false; // Reset snapped flag if dragged too far
            }
        }
    }

    void OnMouseUp()
    {
        collider2d.enabled = false;

        // Snap to nearest drop area if still snapped
        if (isSnapped && snapTransform != null)
        {
            transform.position = snapTransform.position + new Vector3(0, 0, -0.01f);
        }

        collider2d.enabled = true;

        // Reset color after releasing the item if not snapped
        if (!isSnapped)
        {
            spriteRenderer.color = defaultColor;
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }

    Vector3 ClampToViewport(Vector3 position)
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.0f, 1.0f);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.0f, 1.0f);
        return mainCamera.ViewportToWorldPoint(viewportPosition);
    }
}
