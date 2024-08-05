using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ageText;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private float animationDuration = 0.5f; // Duration of the animation
    [SerializeField] private float offscreenPositionX = 1000f; // Position to move offscreen
    [SerializeField] private float centerPositionX = 269f; // Position to move to center

    private int currentCharacter = 0;

    private readonly string[] ageRanges = { "Below 20", "20-29", "30-49", "50-64", "Above 64" };

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

        // Position off-screen based on direction
        float newOffscreenPositionX = direction == 1 ? -offscreenPositionX : offscreenPositionX;

        // Position current character off-screen
        LeanTween.moveLocalX(transform.GetChild(currentCharacter).gameObject, newOffscreenPositionX, animationDuration);

        // Position new character off-screen based on direction
        float oldOffscreenPositionX = direction == 1 ? offscreenPositionX : -offscreenPositionX;
        transform.GetChild(newIndex).localPosition = new Vector3(oldOffscreenPositionX, transform.GetChild(newIndex).localPosition.y, transform.GetChild(newIndex).localPosition.z);

        // Wait for the current character animation to complete
        yield return new WaitForSeconds(animationDuration);

        // Move the new character to the center
        LeanTween.moveLocalX(transform.GetChild(newIndex).gameObject, centerPositionX, animationDuration);

        // Update the character selection
        SelectCharacter(newIndex);
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

        // Update the age text based on the current character index
        ageText.text = ageRanges[currentCharacter];
    }

    public int GetCurrentCharacterIndex()
    {
        return currentCharacter; // Assuming currentCharacter is a variable that holds the current index
    }

    public string GetAgeRange(int index)
    {
        if (index >= 0 && index < ageRanges.Length)
        {
            return ageRanges[index];
        }
        return null;
    }
}
