using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignalBarSwapper : MonoBehaviour
{
    // Array to hold different signal bar sprites
    public Sprite[] signalSprites;

    // Reference to the Image component displaying the signal bars
    public Image signalImage;

    // Minimum and maximum interval for changing the signal strength
    public float minInterval = 1f;
    public float maxInterval = 5f;

    void Start()
    {
        // Start the coroutine to change the signal bars
        StartCoroutine(SwapSignalBars());
    }

    IEnumerator SwapSignalBars()
    {
        while (true)
        {
            // Wait for a random amount of time before swapping the sprite
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Choose a random sprite from the array
            int randomIndex = Random.Range(0, signalSprites.Length);
            signalImage.sprite = signalSprites[randomIndex];
        }
    }
}
