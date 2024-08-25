using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoWidgetManager : MonoBehaviour
{
    [SerializeField] private Image characterImage; // The UI Image component to display the character
    [SerializeField] private Sprite[] maleSprites; // Array of male sprites
    [SerializeField] private Sprite[] femaleSprites; // Array of female sprites

    // PlayerPrefs keys
    private string playerGenderKey = "playerGender";
    private string playerCharacterIndexKey = "playerCharacterIndex";

    private void Start()
    {
        LoadCharacterImage();
    }

    private void LoadCharacterImage()
    {
        // Get the saved gender and character index from PlayerPrefs
        string selectedGender = PlayerPrefs.GetString(playerGenderKey, "Male");
        int selectedCharacterIndex = PlayerPrefs.GetInt(playerCharacterIndexKey, 0);

        // Set the appropriate sprite based on the saved data
        if (selectedGender == "Male")
        {
            if (selectedCharacterIndex >= 0 && selectedCharacterIndex < maleSprites.Length)
            {
                characterImage.sprite = maleSprites[selectedCharacterIndex];
            }
            else
            {
                Debug.LogError("Character index out of range for male sprites.");
            }
        }
        else if (selectedGender == "Female")
        {
            if (selectedCharacterIndex >= 0 && selectedCharacterIndex < femaleSprites.Length)
            {
                characterImage.sprite = femaleSprites[selectedCharacterIndex];
            }
            else
            {
                Debug.LogError("Character index out of range for female sprites.");
            }
        }
        else
        {
            Debug.LogError("Invalid gender loaded from PlayerPrefs.");
        }
    }
}
