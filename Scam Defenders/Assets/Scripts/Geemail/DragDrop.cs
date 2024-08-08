using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 offset;
    private RectTransform rectTransform;
    private Canvas canvas;

    public string destinationTag = "DropArea";
    public string hotbarTag = "Hotbar";

    public Sprite snappedSprite; // Add this for the snapped sprite

    private Transform snapTransform;
    private DropArea snapDropArea;
    private DropArea previousDropArea;

    private bool isSnapped = false;

    public Image flagRenderer;
    private Vector2 originalPosition;
    private Sprite originalSprite; // To store the original sprite

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        flagRenderer = GetComponent<Image>();
        originalPosition = rectTransform.anchoredPosition; // Store the starting position of the flag
        originalSprite = flagRenderer.sprite; // Store the original sprite
        flagRenderer.raycastTarget = true; // Ensure Raycast Target is enabled
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = rectTransform.anchoredPosition - GetAnchoredPosition(eventData);
        isSnapped = false;

        // Reset to original image
        flagRenderer.sprite = originalSprite;

        // Release the drop area if the flag is dragged out
        if (snapDropArea != null)
        {
            snapDropArea.isOccupied = false;
            snapDropArea = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = GetAnchoredPosition(eventData) + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = eventData.position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        bool foundDropArea = false;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag(destinationTag) && !result.gameObject.CompareTag(hotbarTag))
            {
                DropArea newDropArea = result.gameObject.GetComponent<DropArea>();
                if (!newDropArea.isOccupied)
                {
                    snapTransform = result.gameObject.transform;
                    snapDropArea = result.gameObject.GetComponent<DropArea>();
                    isSnapped = true;

                    rectTransform.anchoredPosition = ((RectTransform)snapTransform).anchoredPosition;
                    snapDropArea.isOccupied = true; // Mark the drop area as occupied
                    previousDropArea = snapDropArea; // Update the previous drop area

                    flagRenderer.sprite = snappedSprite; // Change to snapped sprite

                    // Ensure Raycast Target is enabled
                    flagRenderer.raycastTarget = true;

                    foundDropArea = true;
                    break;
                }
            }
        }

        if (!foundDropArea)
        {
            ResetToOriginalPosition();
        }
    }

    void ResetToOriginalPosition()
    {
        snapTransform = null;
        snapDropArea = null;
        isSnapped = false;

        // Reset to original position and image if not snapped to a new drop area
        rectTransform.anchoredPosition = originalPosition;
        flagRenderer.sprite = originalSprite;
    }

    Vector2 GetAnchoredPosition(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPoint);
        return localPoint;
    }
}
