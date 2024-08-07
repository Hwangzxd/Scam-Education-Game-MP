using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ageText;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private float animationDuration = 0.5f; // Duration of the animation
    [SerializeField] private float offscreenPositionX = 1000f; // Position to move offscreen
    [SerializeField] private float centerPositionX = 269f; // Position to move to center

    [SerializeField] private Sprite[] maleSprites; // Array of male sprites
    [SerializeField] private Sprite[] femaleSprites; // Array of female sprites

    private int currentCharacter = 0;
    private string selectedGender; // Store the selected gender
    private Image[] characterImages; // Array of Image components to display the characters

    private readonly string[] ageRanges = { "Below 20", "20-29", "30-49", "50-64", "Above 64" };

    private void OnEnable()
    {
        // Retrieve the updated gender preference from PlayerPrefs
        selectedGender = PlayerPrefs.GetString("playerGender", "None");

        //Debug.Log(selectedGender);

        // Get all Image components in the child objects
        characterImages = GetComponentsInChildren<Image>();

        // Ensure the character sprites are updated based on the current selection
        SelectCharacter(currentCharacter);
    }

    private void Start()
    {
        // Add listeners to the buttons
        leftArrow.onClick.AddListener(() => StartCoroutine(AnimateCharacter(-1)));
        rightArrow.onClick.AddListener(() => StartCoroutine(AnimateCharacter(1)));

        // Ensure the gender is set before using it
        if (string.IsNullOrEmpty(selectedGender))
        {
            Debug.LogError("Gender not set. Ensure gender is selected before using CharacterSelect.");
        }
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
        //Debug.Log($"Selecting character with index: {index}");

        // Ensure the index is within bounds
        index = Mathf.Clamp(index, 0, transform.childCount - 1);
        //Debug.Log($"Clamped index: {index}");

        // Update the current character index
        currentCharacter = index;

        // Show the selected character and hide others
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }

        // Update the age text based on the current character index
        if (ageText != null)
        {
            if (currentCharacter >= 0 && currentCharacter < ageRanges.Length)
            {
                ageText.text = ageRanges[currentCharacter];
            }
            else
            {
                Debug.LogError("Current character index is out of range.");
            }
        }

        // Update the character sprites for all age groups
        UpdateCharacterSprites();
    }

    private void UpdateCharacterSprites()
    {
        Debug.Log("Updating character sprites.");
        foreach (var img in characterImages)
        {
            if (selectedGender == "Male")
            {
                if (currentCharacter >= 0 && currentCharacter < maleSprites.Length)
                {
                    img.sprite = maleSprites[currentCharacter];
                    Debug.Log($"Male sprite set to: {maleSprites[currentCharacter].name}");
                }
                else
                {
                    Debug.LogError("Current character index is out of range for male sprites.");
                }
            }
            else if (selectedGender == "Female")
            {
                if (currentCharacter >= 0 && currentCharacter < femaleSprites.Length)
                {
                    img.sprite = femaleSprites[currentCharacter];
                    Debug.Log($"Female sprite set to: {femaleSprites[currentCharacter].name}");
                }
                else
                {
                    Debug.LogError("Current character index is out of range for female sprites.");
                }
            }
            else
            {
                Debug.LogError("Selected gender is not valid.");
            }
        }
    }

    public void SetGender(string gender)
    {
        if (gender != "Male" && gender != "Female")
        {
            Debug.LogError("Invalid gender selected.");
            return;
        }

        selectedGender = gender;
        PlayerPrefs.SetString("playerGender", selectedGender);
        PlayerPrefs.Save();
        SelectCharacter(currentCharacter); // Refresh the character sprite
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
