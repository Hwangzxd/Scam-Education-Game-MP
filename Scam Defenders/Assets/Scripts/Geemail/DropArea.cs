using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOccupied = false;
    public bool isHotbar = false;
    public Color highlightColor = new Color(1, 1, 1, 0.5f); // Adjust transparency as needed
    public GameObject[] highlightObjects; // Array of external highlight objects

    private Color[] originalColors;
    private Image[] highlightImages;

    private void Start()
    {
        highlightImages = new Image[highlightObjects.Length];
        originalColors = new Color[highlightObjects.Length];

        for (int i = 0; i < highlightObjects.Length; i++)
        {
            if (highlightObjects[i] != null)
            {
                highlightImages[i] = highlightObjects[i].GetComponent<Image>();

                if (highlightImages[i] != null)
                {
                    originalColors[i] = highlightImages[i].color;
                }
                else
                {
                    Debug.LogError($"DropArea: HighlightObject at index {i} does not have an Image component.");
                }
            }
            else
            {
                Debug.LogError($"DropArea: No HighlightObject assigned at index {i}.");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHotbar && !isOccupied && eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Draggable"))
        {
            Debug.Log("Draggable entered drop area.");
            HighlightDropArea(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHotbar && !isOccupied && eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Draggable"))
        {
            Debug.Log("Draggable exited drop area.");
            HighlightDropArea(false);
        }
    }

    private void HighlightDropArea(bool highlight)
    {
        for (int i = 0; i < highlightImages.Length; i++)
        {
            if (highlightImages[i] != null)
            {
                highlightImages[i].color = highlight ? highlightColor : originalColors[i];
                highlightImages[i].enabled = highlight; // Optionally enable/disable the highlight object
            }
        }
    }
}
