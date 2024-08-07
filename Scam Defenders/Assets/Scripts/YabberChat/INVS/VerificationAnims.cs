using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificationAnims : MonoBehaviour
{
    public GameObject Panel;
    private RectTransform panelRectTransform;

    private void Start()
    {
        // Get the RectTransform component of the openedPanel GameObject
        panelRectTransform = Panel.GetComponent<RectTransform>();
    }

    public void Open()
    {
        // Animate the Y position of the Panel to -28 over 0.5 seconds
        LeanTween.moveY(panelRectTransform, -70f, 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    public void Close()
    {
        // Animate the Y position of the Panel to -471 over 0.5 seconds
        LeanTween.moveY(panelRectTransform, -471f, 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }
}
