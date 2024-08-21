using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro object
    public float delay = 0.05f; // Delay between each letter

    private string fullText;

    private void Start()
    {
        // Store the existing text from the TextMeshPro component
        fullText = dialogueText.text;

        // Clear the text so it starts empty
        dialogueText.text = "";

        // Start the typewriter effect
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            dialogueText.text += letter; // Add each letter progressively
            yield return new WaitForSeconds(delay); // Wait before adding the next letter
        }
    }
}
