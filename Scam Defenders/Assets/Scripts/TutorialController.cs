using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialController : MonoBehaviour
{
    public Button nextButton; // Reference to the button for the current tutorial screen
    public float delayDuration = 1f; // Delay duration in seconds

    void OnEnable()
    {
        DisableButton();
        StartCoroutine(EnableButtonAfterDelay());
    }

    void DisableButton()
    {
        nextButton.interactable = false;
    }

    IEnumerator EnableButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayDuration);
        nextButton.interactable = true;
    }
}