using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer instance;
    public GameObject bar;
    public GameObject timesUp;
    public int time;

    public float delay = 2f;

    public LevelLoader levelLoader; // Reference to the LevelLoader script

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Make sure timeText is not destroyed
            if (bar != null)
            {
                DontDestroyOnLoad(bar.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    //private IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(delay);
    //}

    public void TimesUp()
    {
        //timesUp.SetActive(true);
        //StartCoroutine(Delay());
        levelLoader.LoadNextLevel(); // Call LoadNextLevel when the timer ends
    }
}
