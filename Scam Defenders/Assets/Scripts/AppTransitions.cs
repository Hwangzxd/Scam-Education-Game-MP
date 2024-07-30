using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppTransitions : MonoBehaviour
{
    public TextMeshProUGUI temperature;
    public TextMeshProUGUI description;
    public Image weatherImg;

    public void EndTransition()
    {
        this.gameObject.SetActive(false);
    }

    public void WeatherShow()
    {
        temperature.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void WeatherHide()
    {
        weatherImg.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
