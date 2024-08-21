using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBubble : MonoBehaviour
{
    private Image backgroundImage;
    private Image policeImage;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        backgroundImage = transform.Find("Background").GetComponent<Image>();
        policeImage = transform.Find("Police").GetComponent<Image>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Setup("Hello World");
    }

    private void Setup(string text)
    {
        textMeshPro.SetText(text);
    }
}
