using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsManagerGois : MonoBehaviour
{
    public DialogueManagerGois DialogueManagerGois;

    public Button button1;
    public Button button2;

    public TextMeshProUGUI textName;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "YabberChatGOIS")
        {
            textName.text = "+65 9837 3949";
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatGOIS1")
        {
            textName.text = "+65 8765 4321";
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatGOIS2")
        {
            textName.text = "gov.sg";
        }
        // Assign listeners to the buttons
        button1.onClick.AddListener(OnButton1Click);
        button2.onClick.AddListener(OnButton2Click);

    }

    //main buttons
    public void OnButton1Click()
    {
        disableAllButtons();

        StartCoroutine(DialogueManagerGois.lose());

    }

    public void OnButton2Click()
    {
        disableAllButtons();

        StartCoroutine(DialogueManagerGois.win());
    }

    //helper methods
    public void disableAllButtons()
    {
        button1.enabled = false;
        button2.enabled = false;
    }

    public void enableAllButtons()
    {
        button1.enabled = true;
        button2.enabled = true;
    }

}
