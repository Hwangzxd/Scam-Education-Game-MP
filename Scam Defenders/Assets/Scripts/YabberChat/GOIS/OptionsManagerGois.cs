using UnityEngine;
using UnityEngine.UI;

public class OptionsManagerGois : MonoBehaviour
{
    public DialogueManagerGois DialogueManagerGois;

    public Button button1;
    public Button button2;

    void Start()
    {
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
