using UnityEngine;
using TMPro;

public class Hotbar : MonoBehaviour
{
    public TMP_Text itemCountText; 

    public int itemCount = 0;
    public string draggableTag = "Draggable"; 
    public float detectionRadius = 0.5f; 

    void Start()
    {
        UpdateItemCountText();
    }

    void Update()
    {
        CheckForDroppedItems();
    }

    void CheckForDroppedItems()
    {
        //Perform a circle cast to detect draggable items within the detection radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        int newCount = 0;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(draggableTag))
            {
                newCount++;
            }
        }

        //Updates the item count if it has changed
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
