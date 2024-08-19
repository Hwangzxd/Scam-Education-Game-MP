using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BirdyMsgs : MonoBehaviour
{
    // Reference to the ScriptableObject
    public BirdyData birdyData;

    // UI elements that need to be updated
    public Image senderImage;            // Reference to the Image component for the sender's avatar
    public TextMeshProUGUI senderText;   // Reference to the TextMeshProUGUI component for the sender's name

    public void Start()
    {
        SetScenarioAvatarsAndNames(1);
    }
    public void SetScenarioAvatarsAndNames(int scenarioNumber)
    {
        string playerGender = PlayerPrefs.GetString("playerGender");

        if (string.IsNullOrEmpty(playerGender))
        {
            Debug.LogWarning("No gender saved in PlayerPrefs");
            return; // Exit the method if no gender is saved
        }

        switch (scenarioNumber)
        {
            case 1:
                if (playerGender == "Male")
                {
                    senderImage.sprite = birdyData.girlAvatar1;
                    senderText.text = birdyData.girlName1;
                }
                else if (playerGender == "Female")
                {
                    senderImage.sprite = birdyData.guyAvatar1;
                    senderText.text = birdyData.guyName1;
                }
                break;

            case 2:
                if (playerGender == "Male")
                {
                    senderImage.sprite = birdyData.girlAvatar2;
                    senderText.text = birdyData.girlName2;
                }
                else if (playerGender == "Female")
                {
                    senderImage.sprite = birdyData.guyAvatar2;
                    senderText.text = birdyData.guyName2;
                }
                break;

            case 3:
                if (playerGender == "Male")
                {
                    senderImage.sprite = birdyData.girlAvatar3;
                    senderText.text = birdyData.girlName3;
                }
                else if (playerGender == "Female")
                {
                    senderImage.sprite = birdyData.guyAvatar3;
                    senderText.text = birdyData.guyName3;
                }
                break;

            default:
                Debug.LogWarning("Invalid scenario number");
                break;
        }
    }

}
