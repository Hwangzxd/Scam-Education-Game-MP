using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class CountdownTimer : MonoBehaviour
{
    public GameObject bar;
    public GameObject timesUp;
    public int time;

    public float delay = 2f;

    public LevelLoader levelLoader; // Reference to the LevelLoader script

    public void Start()
    {
        //AnimateBar();
        //StartCoroutine(Countdown());
    }

    public void StartGame()
    {
        AnimateBar();
        StartCoroutine(Countdown());
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 0, time);
    }

    private IEnumerator Countdown()
    {
        float currentTime = time;
        while (currentTime > 0)
        {
            yield return null; // Wait for the next frame
            currentTime -= Time.deltaTime;
        }
        TimesUp();
    }

    public void TimesUp()
    {
        timesUp.SetActive(true);
        StartCoroutine(LoadNextLevelAfterDelay());
    }

    private IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        levelLoader.LoadNextLevel();
    }
}
