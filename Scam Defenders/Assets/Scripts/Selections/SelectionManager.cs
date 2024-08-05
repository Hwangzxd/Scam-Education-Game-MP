using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public SceneTransitions sceneTransitions;
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

    // Age Selection
    [SerializeField] private CharacterSelect characterSelect;
    [HideInInspector] public string playerAgeKey = "playerAge";
    string playerAge;
    [HideInInspector] public string playerScenarioKey = "playerScenario";
    int playerScenario;

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
        // Debug.Log($"Player Name Input Changed: {input}, Name Entered: {nameEntered}");
        UpdateConfirmButtonState();
    }

    public void SaveName()
    {
        if (nameEntered)
        {
            playerName = playerNameInput.text;
            PlayerPrefs.SetString(playerNameKey, playerName);
            Debug.Log($"Saved Player Name: {playerName}");
        }
        else
        {
            // Debug.LogWarning("No name entered to save.");
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
        //Debug.Log($"Gender Selected: {playerGender}");

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

    #region AgeSelection

    public void SaveAge()
    {
        int selectedIndex = characterSelect.GetCurrentCharacterIndex();
        playerAge = characterSelect.GetAgeRange(selectedIndex);
        PlayerPrefs.SetString(playerAgeKey, playerAge);

        // Determine the scenario based on the age range
        playerScenario = (selectedIndex == 0 || selectedIndex == 1) ? 1 : 2;
        PlayerPrefs.SetInt(playerScenarioKey, playerScenario);

        Debug.Log($"Saved Player Age: {playerAge}");
        Debug.Log($"Assigned Scenario: {playerScenario}");
    }

    public string GetPlayerAge()
    {
        return PlayerPrefs.GetString(playerAgeKey);
    }

    public int GetPlayerScenario()
    {
        return PlayerPrefs.GetInt(playerScenarioKey);
    }

    #endregion

    #region ConfirmButton

    private void UpdateConfirmButtonState()
    {
        bool nameScreenActive = nameSelection.activeSelf;
        bool genderScreenActive = genderSelection.activeSelf;
        bool ageScreenActive = ageSelection.activeSelf;

        // Enable the confirm button if the name is entered in the name selection screen or the gender is selected in the gender selection screen
        if ((nameScreenActive && nameEntered) || (genderScreenActive && genderSelected) || ageScreenActive)
        {
            SetConfirmButtonOpacity(1.0f);
            confirmButton.enabled = true;
            // Debug.Log("Confirm Button Enabled");
        }
        else
        {
            SetConfirmButtonOpacity(0.75f);
            confirmButton.enabled = false;
            // Debug.Log("Confirm Button Disabled");
        }
    }

    private void SetConfirmButtonOpacity(float opacity)
    {
        confirmButtonColor.a = opacity;
        confirmButtonImage.color = confirmButtonColor;
        // Debug.Log($"Confirm Button Opacity Set To: {opacity}");
    }

    public void Confirm()
    {
        if (nameSelection.activeSelf)
        {
            SaveName();
            //Debug.Log($"Player Name Confirmed: {GetPlayerName()}");

            nameSelection.SetActive(false);
            genderSelection.SetActive(true);
            // Debug.Log("Switched to Gender Selection Screen");

            // Reset the confirm button state for gender selection screen
            UpdateConfirmButtonState();
        }
        else if (genderSelection.activeSelf)
        {
            Debug.Log($"Player Gender Confirmed: {GetPlayerGender()}");

            genderSelection.SetActive(false);
            ageSelection.SetActive(true);
            // Debug.Log("Switched to Age Selection Screen");

            // Reset the confirm button state for age selection screen (if applicable)
            UpdateConfirmButtonState();
        }
        else if (ageSelection.activeSelf)
        {
            SaveAge();
            //Debug.Log($"Player Age Confirmed: {GetPlayerAge()}");
            //Debug.Log($"Scenario Assigned: {GetPlayerScenario()}");

            sceneTransitions.GoToHome();
        }
    }

    private void SetButtonOpacity(Image buttonImage, float opacity)
    {
        Color color = buttonImage.color;
        color.a = opacity;
        buttonImage.color = color;
        // Debug.Log($"Button Opacity Set To: {opacity}");
    }

    #endregion

    public void Back()
    {
        if (nameSelection.activeSelf)
        {
            sceneTransitions.GoToStart();
        }
        else if (genderSelection.activeSelf)
        {
            genderSelection.SetActive(false);
            ageSelection.SetActive(false);
            nameSelection.SetActive(true);
        }
        else if (ageSelection.activeSelf)
        {
            ageSelection.SetActive(false);
            nameSelection.SetActive(false);
            genderSelection.SetActive(true);
        }
    }
}
