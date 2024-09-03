using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InfoManager : MonoBehaviour
{
    public string[] infoOptions;        // Array holding info strings
    public TextMeshProUGUI infoText;    // Reference to the TextMeshProUGUI component
    public float textLoadSpeed = 0.05f; // Speed at which text is displayed
    private int currentInfoIndex = 0;
    private bool isTextLoading = false; // Flag to prevent spamming

    void Start()
    {
        ShowNextInfo(); // Start the info sequence
    }

    void ShowNextInfo()
    {
        if (currentInfoIndex < infoOptions.Length)
        {
            if (isTextLoading)
            {
                StopAllCoroutines(); // Stop current text loading coroutine
                DisplayTextInstantly(infoOptions[currentInfoIndex]); // Instant text update
            }
            else
            {
                StartCoroutine(DisplayInfo(infoOptions[currentInfoIndex]));
            }
        }
        else
        {
            GoToHomeScreen(); // No more info, go back to the home screen
        }
    }

    IEnumerator DisplayInfo(string info)
    {
        isTextLoading = true; // Prevent further calls until text is fully loaded
        infoText.text = ""; // Clear existing text
        foreach (char letter in info.ToCharArray())
        {
            infoText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(textLoadSpeed);
        }
        isTextLoading = false; // Allow the next text to be loaded
    }

    public void LoadNextInfo()
    {
        currentInfoIndex++;
        ShowNextInfo(); // Show the next info or instant text update
    }

    public void SkipAnimation()
    {
        if (isTextLoading)
        {
            StopAllCoroutines(); // Stop current text loading coroutine
            DisplayTextInstantly(infoOptions[currentInfoIndex]); // Instant text update
        }
    }

    void DisplayTextInstantly(string info)
    {
        infoText.text = info; // Set the text instantly
        isTextLoading = false; // Allow the next text to be loaded
    }

    void GoToHomeScreen()
    {
        SceneManager.LoadScene("Home"); // Load the home screen scene
    }
}
