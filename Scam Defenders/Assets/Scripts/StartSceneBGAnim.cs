using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneBGAnim : MonoBehaviour
{
    public RectTransform cloud;
    public float moveDistance = 1200f; // Distance to move left and right
    public float moveDuration = 5f;   // Duration for each move

    void Start()
    {
        // Start the left and right movement
        MoveCloud();
    }

    void MoveCloud()
    {
        // Move the cloud to the left
        LeanTween.moveX(cloud, cloud.anchoredPosition.x - moveDistance, moveDuration).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
        {
            // Move the cloud to the right once it reaches the left position
            LeanTween.moveX(cloud, cloud.anchoredPosition.x + moveDistance, moveDuration).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
            {
                // Repeat the movement
                MoveCloud();
            });
        });
    }
}
