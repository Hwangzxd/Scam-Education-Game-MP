using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    Collider2D collider2d;

    public string destinationTag = "DropArea";
    public Color defaultColor = Color.white;
    public Color snapColor = Color.red;

    private Camera mainCamera;
    private Transform snapTransform;
    private DropArea snapDropArea;
    private DropArea previousDropArea; // To track the previous drop area

    private bool isSnapped = false;
    private bool isInPlace = false;

    public SpriteRenderer spriteRenderer;
    private Vector3 originalPosition; // Store the original position of the flag

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position; // Store the starting position of the flag
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        snapTransform = null;
        snapDropArea = null;
        isSnapped = false;
        isInPlace = false;
        spriteRenderer.color = defaultColor;

        // Store the previous drop area
        if (previousDropArea != null)
        {
            previousDropArea.isOccupied = false;
            previousDropArea = null;
        }
    }

    void OnMouseDrag()
    {
        Vector3 newPos = MouseWorldPosition() + offset;
        newPos = ClampToViewport(newPos);
        transform.position = newPos;

        // Reset color while dragging
        spriteRenderer.color = defaultColor;
    }

    void OnMouseUp()
    {
        collider2d.enabled = false;

        // Release from previous drop area if snapped
        if (isSnapped && snapTransform != null)
        {
            snapDropArea.isOccupied = false;
            snapDropArea = null;
        }

        // Perform raycast to find new drop area
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag(destinationTag))
        {
            snapTransform = hit.transform;
            snapDropArea = hit.collider.GetComponent<DropArea>();
            isSnapped = true;

            transform.position = snapTransform.position + new Vector3(0, 0, -0.01f);
            isInPlace = true;
            snapDropArea.isOccupied = true; // Mark the drop area as occupied
            previousDropArea = snapDropArea; // Update the previous drop area
        }
        else
        {
            snapTransform = null;
            snapDropArea = null;
            isSnapped = false;
            isInPlace = false;

            // Reset to original position if not snapped to a new drop area
            transform.position = originalPosition;
        }

        collider2d.enabled = true;
        UpdateColor();
    }

    void UpdateColor()
    {
        if (isInPlace)
        {
            spriteRenderer.color = snapColor;
        }
        else
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
