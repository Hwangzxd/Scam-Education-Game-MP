using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private float animationDuration = 0.5f; // Duration of the animation
    [SerializeField] private float offscreenPosition = 1000f; // Position to move offscreen
    [SerializeField] private float centerPosition = 0f; // Position to move to center

    private int currentCharacter = 0;

    private void Start()
    {
        // Initialize by selecting the first character
        SelectCharacter(0);

        // Add listeners to the buttons
        leftArrow.onClick.AddListener(() => StartCoroutine(AnimateCharacter(-1)));
        rightArrow.onClick.AddListener(() => StartCoroutine(AnimateCharacter(1)));
    }

    private IEnumerator AnimateCharacter(int direction)
    {
        // Calculate the new index
        int newIndex = (currentCharacter + direction + transform.childCount) % transform.childCount;

        // Get the current position of the characters
        Vector3 offscreenPosition = direction == 1 ? new Vector3(this.transform.position.x - this.transform.GetComponent<RectTransform>().rect.width, this.transform.position.y, this.transform.position.z) : new Vector3(this.transform.position.x + this.transform.GetComponent<RectTransform>().rect.width, this.transform.position.y, this.transform.position.z);

        // Move the current character offscreen
        LeanTween.moveLocalX(gameObject, direction == 1 ? offscreenPosition.x : -offscreenPosition.x, animationDuration);

        // Wait for the animation to complete
        yield return new WaitForSeconds(animationDuration);

        // Update the character selection
        SelectCharacter(newIndex);

        // Move the new character into the center
        LeanTween.moveLocalX(gameObject, centerPosition, animationDuration);
    }

    private void SelectCharacter(int index)
    {
        // Ensure the index is within bounds
        index = Mathf.Clamp(index, 0, transform.childCount - 1);

        // Update the current character index
        currentCharacter = index;

        // Show the selected character and hide others
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }
}
