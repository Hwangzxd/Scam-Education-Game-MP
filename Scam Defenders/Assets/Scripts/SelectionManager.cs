using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Button confirmButton;

    private Image confirmButtonImage;
    private Color confirmButtonColor;

    // Player Name
    [SerializeField] TMP_InputField playerNameInput;
    [HideInInspector] public string playerNameKey = "playerName";
    string playerName;
    bool nameEntered = false;

    // Gender Selection
    [SerializeField] Button maleButton;
    [SerializeField] Button femaleButton;
    [HideInInspector] public string playerGenderKey = "playerGender";
    string playerGender;

    private Image maleButtonImage;
    private Image femaleButtonImage;
    private Color maleButtonColor;
    private Color femaleButtonColor;
    private bool genderSelected = false;

    // Screens
    public GameObject nameSelection;
    public GameObject genderSelection;
    public GameObject ageSelection;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Initialize the confirm button image and set initial opacity to 75%
        confirmButtonImage = confirmButton.GetComponent<Image>();
        confirmButtonColor = confirmButtonImage.color;
        SetConfirmButtonOpacity(0.75f);
        confirmButton.enabled = false;

        // Set the character limit for the input field
        playerNameInput.characterLimit = 12;

        // Add a listener to the input field to monitor text changes
        playerNameInput.onValueChanged.AddListener(OnPlayerNameInputChanged);

        // Initialize the gender buttons images and set initial opacity to 50%
        maleButtonImage = maleButton.GetComponent<Image>();
        femaleButtonImage = femaleButton.GetComponent<Image>();
        maleButtonColor = maleButtonImage.color;
        femaleButtonColor = femaleButtonImage.color;
        SetGenderButtonsOpacity(0.8f);

        // Add listeners to the gender buttons
        maleButton.onClick.AddListener(() => OnGenderButtonClick("Male"));
        femaleButton.onClick.AddListener(() => OnGenderButtonClick("Female"));
    }

    #region PlayerNameInput

    private void OnPlayerNameInputChanged(string input)
    {
        nameEntered = !string.IsNullOrWhiteSpace(input);
        UpdateConfirmButtonState();
    }

    public void SaveName()
    {
        if (nameEntered)
        {
            playerName = playerNameInput.text;
            PlayerPrefs.SetString(playerNameKey, playerName);
        }
    }

    public string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey);
    }

    #endregion

    #region GenderSelection

    private void OnGenderButtonClick(string gender)
    {
        genderSelected = true;
        playerGender = gender;
        PlayerPrefs.SetString(playerGenderKey, playerGender);

        if (gender == "Male")
        {
            SetButtonOpacity(maleButtonImage, 1.0f);
            SetButtonOpacity(femaleButtonImage, 0.8f);
        }
        else if (gender == "Female")
        {
            SetButtonOpacity(maleButtonImage, 0.8f);
            SetButtonOpacity(femaleButtonImage, 1.0f);
        }

        UpdateConfirmButtonState();
    }

    public string GetPlayerGender()
    {
        return PlayerPrefs.GetString(playerGenderKey);
    }

    private void SetGenderButtonsOpacity(float opacity)
    {
        SetButtonOpacity(maleButtonImage, opacity);
        SetButtonOpacity(femaleButtonImage, opacity);
    }

    #endregion

    #region ConfirmButton

    private void UpdateConfirmButtonState()
    {
        // Enable the confirm button if the name is entered in the name selection screen or the gender is selected in the gender selection screen
        if ((nameSelection.activeSelf && nameEntered) || (genderSelection.activeSelf && genderSelected))
        {
            SetConfirmButtonOpacity(1.0f);
            confirmButton.enabled = true;
        }
        else
        {
            SetConfirmButtonOpacity(0.75f);
            confirmButton.enabled = false;
        }
    }

    private void SetConfirmButtonOpacity(float opacity)
    {
        confirmButtonColor.a = opacity;
        confirmButtonImage.color = confirmButtonColor;
    }

    public void Confirm()
    {
        if (nameSelection.activeSelf)
        {
            SaveName();
            Debug.Log("Player Name: " + GetPlayerName());

            nameSelection.SetActive(false);
            genderSelection.SetActive(true);

            // Reset the confirm button state for gender selection screen
            UpdateConfirmButtonState();
        }
        else if (genderSelection.activeSelf)
        {
            Debug.Log("Player Gender: " + GetPlayerGender());

            genderSelection.SetActive(false);
            ageSelection.SetActive(true);

            // Reset the confirm button state for age selection screen (if applicable)
            UpdateConfirmButtonState();
        }
    }

    private void SetButtonOpacity(Image buttonImage, float opacity)
    {
        Color color = buttonImage.color;
        color.a = opacity;
        buttonImage.color = color;
    }

    #endregion
}
