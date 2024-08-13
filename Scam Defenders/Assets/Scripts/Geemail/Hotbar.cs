using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public TMP_Text itemCountText;
    public int itemCount = 0;
    public string draggableTag = "Draggable";
    public float detectionRadius = 50f; // Adjusted for UI elements (in pixels)

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        UpdateItemCountText();
    }

    void Update()
    {
        CheckForDroppedItems();
    }

    void CheckForDroppedItems()
    {
        // Perform a circle cast using RectTransformUtility to detect UI elements within the detection radius
        RectTransform[] uiElements = FindObjectsOfType<RectTransform>();
        int newCount = 0;

        foreach (RectTransform element in uiElements)
        {
            if (element.CompareTag(draggableTag))
            {
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, element.position, null, out localPoint);

                if (Vector2.Distance(localPoint, rectTransform.rect.center) < detectionRadius)
                {
                    newCount++;
                }
            }
        }

        // Updates the item count if it has changed
        if (newCount != itemCount)
        {
            itemCount = newCount;
            UpdateItemCountText();
        }
    }

    void UpdateItemCountText()
    {
        if (itemCountText != null)
        {
            itemCountText.text = "x" + itemCount;
        }
    }
}
