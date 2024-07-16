using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    Collider2D collider2d;
    public string destinationTag = "DropArea";
    public float snapDistance = 1.0f; 
    public float releaseDistance = 2.0f;

    private Camera mainCamera;
    private Transform snapTransform; 
    private bool isSnapped = false; 

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        snapTransform = null; 
        isSnapped = false; 
    }

    void OnMouseDrag()
    {
        Vector3 newPos = MouseWorldPosition() + offset;
        newPos = ClampToViewport(newPos);
        transform.position = newPos;

        //Check for nearby drop areas within snap distance
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, snapDistance);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag(destinationTag))
            {
                //Calculates distance between center points of item and drop area
                float distance = Vector3.Distance(transform.position, col.transform.position);
                float distanceThreshold = 1.0f; 

                //Snaps only if within distance threshold
                if (distance <= distanceThreshold)
                {
                    snapTransform = col.transform;
                    isSnapped = true; 
                    break;
                }
                else
                {
                    snapTransform = null;
                    isSnapped = false; 
                }
            }
        }

        //Release snap if dragged too far from any drop area 
        //This allows free movement of the flags
        if (snapTransform != null)
        {
            float distanceToSnap = Vector3.Distance(transform.position, snapTransform.position);
            if (distanceToSnap > releaseDistance)
            {
                snapTransform = null;
                isSnapped = false; 
            }
        }
    }

    void OnMouseUp()
    {
        collider2d.enabled = false;

        //Snaps to nearest drop area if still snapped
        if (isSnapped && snapTransform != null)
        {
            transform.position = snapTransform.position + new Vector3(0, 0, -0.01f);
        }

        collider2d.enabled = true;
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
