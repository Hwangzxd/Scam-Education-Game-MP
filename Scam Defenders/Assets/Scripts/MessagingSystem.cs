using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagingSystem : MonoBehaviour
{
    public List<GameObject> sellerMessages; // The object you want to activate
    public GameObject dialogueOptions;
    public Button optionButton; // The button that triggers the activation
    public float delay = 2f; // Delay in seconds

    void Start()
    {
        if (optionButton != null)
        {
            optionButton.onClick.AddListener(StartActivationProcess);
        }
    }

    void StartActivationProcess()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        foreach (GameObject obj in sellerMessages)
        {
            yield return new WaitForSeconds(delay);
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        yield return new WaitForSeconds(delay);
        if (dialogueOptions != null)
        {
            dialogueOptions.SetActive(true);
        }
    }
}
