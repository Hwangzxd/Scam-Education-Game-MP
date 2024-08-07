using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public RectTransform batteryContainer;
    public RectTransform batteryFill; 
    public float widthReductionPercentage = 0.75f; // Percentage to reduce the width (0.75 for -75%)
    public float dropDistance = 350f;

    private Vector2 initialSize;
    private Vector3 initialPosition;

    public GameObject batteryText;
    public GameObject reputationScore;
    public Button mainMenuButton;
    public GameObject policeSprite;

    void Start()
    {
        HideGameOverUI();

        // Store initial size and position
        initialSize = batteryFill.sizeDelta;
        initialPosition = batteryFill.position;

        // Start the battery animation
        AnimateBattery();
    }

    void AnimateBattery()
    {
        // Calculate the target width for the battery fill
        float targetWidth = initialSize.x * (1 - widthReductionPercentage);

        // Animate the battery width reduction
        LeanTween.size(batteryFill, new Vector2(targetWidth, batteryFill.sizeDelta.y), 0.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                // Move the battery fill from the current position to the target drop position
                LeanTween.moveY(batteryContainer.gameObject, batteryContainer.position.y - dropDistance, 0.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
                {
                    // Add a bounce effect after dropping
                    LeanTween.moveY(batteryContainer.gameObject, batteryContainer.position.y - dropDistance + 50f, 0.3f).setEase(LeanTweenType.easeOutBounce).setOnComplete(ShowGameOverUI);
                });
        });
    }

    void HideGameOverUI()
    {
        // Activate Game Over text
        batteryText.SetActive(false);
        reputationScore.SetActive(false);

        // Activate buttons
        mainMenuButton.gameObject.SetActive(false);

        policeSprite.SetActive(false);
    }

    void ShowGameOverUI()
    {
        // Activate and animate Game Over text
        batteryText.SetActive(true);
        LeanTween.scale(batteryText, Vector3.one, 0.8f).setFrom(Vector3.zero).setEase(LeanTweenType.easeOutBack);

        // Activate and animate buttons
        reputationScore.gameObject.SetActive(true);
        LeanTween.scale(reputationScore.gameObject, Vector3.one, 0.8f).setFrom(Vector3.zero).setEase(LeanTweenType.easeOutBack);

        mainMenuButton.gameObject.SetActive(true);
        LeanTween.scale(mainMenuButton.gameObject, Vector3.one, 0.8f).setFrom(Vector3.zero).setEase(LeanTweenType.easeOutBack);

        // Activate and animate the police sprite
        policeSprite.SetActive(true);
        LeanTween.scale(policeSprite, Vector3.one, 0.8f).setFrom(Vector3.zero).setEase(LeanTweenType.easeOutBack);
    }
}
