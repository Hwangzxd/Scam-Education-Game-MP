using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject shield;
    public Sprite brokenShieldSprite;
    public GameObject gameOverText;
    public Button tryAgainButton;
    public Button mainMenuButton;
    public GameObject policeSprite;

    void Start()
    {
        HideGameOverUI();

        // Start the shield animation
        AnimateShield();
    }

    void AnimateShield()
    {
        // Calculate the target Y position for the shield to stop at the bottom
        float shieldHeight = shield.GetComponent<SpriteRenderer>().bounds.size.y;

        // Adjust this value to control the distance the shield drops
        float dropDistance = 1300f;

        // Move the shield from the top to the target Y position quickly to simulate a high drop
        LeanTween.moveY(shield, shield.transform.position.y - dropDistance, 0.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
        {
            // Add a slight bounce effect after hitting the "ground"
            LeanTween.moveY(shield, shield.transform.position.y - dropDistance + 50f, 0.3f).setEase(LeanTweenType.easeOutBounce).setOnComplete(() =>
            {
                // Change the sprite to the broken shield using Image component
                Image shieldImage = shield.GetComponent<Image>();
                if (shieldImage != null)
                {
                    shieldImage.sprite = brokenShieldSprite;
                }

                // Rotate the shield by 45 degrees slowly after impact
                LeanTween.rotateZ(shield, 45f, 0.3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(ShowGameOverUI);
            });
        });
    }

    void HideGameOverUI()
    {
        // Activate Game Over text
        gameOverText.SetActive(false);

        // Activate buttons
        tryAgainButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

        policeSprite.SetActive(false);
    }

    void ShowGameOverUI()
    {
        // Activate and animate Game Over text
        gameOverText.SetActive(true);
        LeanTween.scale(gameOverText, Vector3.one, 0.8f).setFrom(Vector3.zero).setEase(LeanTweenType.easeOutBack);

        // Activate and animate buttons
        tryAgainButton.gameObject.SetActive(true);
        LeanTween.scale(tryAgainButton.gameObject, Vector3.one, 0.8f).setFrom(Vector3.zero).setEase(LeanTweenType.easeOutBack);

        mainMenuButton.gameObject.SetActive(true);
        LeanTween.scale(mainMenuButton.gameObject, Vector3.one, 0.8f).setFrom(Vector3.zero).setEase(LeanTweenType.easeOutBack);

        // Activate and animate the police sprite
        policeSprite.SetActive(true);
        LeanTween.scale(policeSprite, Vector3.one, 0.8f).setFrom(Vector3.zero).setEase(LeanTweenType.easeOutBack);
    }
}
