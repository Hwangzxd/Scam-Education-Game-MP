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
    public Color defaultColor = Color.white;
    public Color snapColor = Color.red;
    public Color hoverColor = Color.yellow;

    private Transform snapTransform;
    private DropArea snapDropArea;
    private DropArea previousDropArea;

    private bool isSnapped = false;
    private bool isInPlace = false;

    public Image flagRenderer;
    private Vector2 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        flagRenderer = GetComponent<Image>();
        originalPosition = rectTransform.anchoredPosition; // Store the starting position of the flag
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = rectTransform.anchoredPosition - GetAnchoredPosition(eventData);
        snapTransform = null;
        snapDropArea = null;
        isSnapped = false;
        isInPlace = false;
        flagRenderer.color = defaultColor;

        // Store the previous drop area
        if (previousDropArea != null)
        {
            previousDropArea.isOccupied = false;
            previousDropArea = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = GetAnchoredPosition(eventData) + offset;
        flagRenderer.color = defaultColor;

        // Perform raycast to check for hover over drop area
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = eventData.position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag(destinationTag))
            {
                DropArea hoverDropArea = result.gameObject.GetComponent<DropArea>();
                if (!result.gameObject.CompareTag(hotbarTag) && !hoverDropArea.isOccupied)
                {
                    flagRenderer.color = hoverColor; // Change color on hover
                }
                break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Release from previous drop area if snapped
        if (isSnapped && snapTransform != null)
        {
            snapDropArea.isOccupied = false;
            snapDropArea = null;
        }

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = eventData.position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

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
                    isInPlace = true;
                    snapDropArea.isOccupied = true; // Mark the drop area as occupied
                    previousDropArea = snapDropArea; // Update the previous drop area
                    break;
                }
                else
                {
                    ResetToOriginalPosition();
                }
            }
            else
            {
                ResetToOriginalPosition();
            }
        }

        UpdateColor();
    }

    void ResetToOriginalPosition()
    {
        snapTransform = null;
        snapDropArea = null;
        isSnapped = false;
        isInPlace = false;

        // Reset to original position if not snapped to a new drop area
        rectTransform.anchoredPosition = originalPosition;
    }

    void UpdateColor()
    {
        if (isInPlace)
        {
            flagRenderer.color = snapColor;
        }
        else
        {
            flagRenderer.color = defaultColor;
        }
    }

    Vector2 GetAnchoredPosition(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPoint);
        return localPoint;
    }
}
