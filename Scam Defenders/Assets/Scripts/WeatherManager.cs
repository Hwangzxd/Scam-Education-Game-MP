using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class WeatherManager : MonoBehaviour
{
    public string apiKey = "cb00cc463151ff633fd3ec1774245f7c";
    public string city = "Singapore";
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI weatherDescriptionText;

    private string apiUrl;

    // Define classes to match the JSON structure
    [System.Serializable]
    public class WeatherData
    {
        public Main main;
        public Weather[] weather;
    }

    [System.Serializable]
    public class Main
    {
        public float temp;
    }

    [System.Serializable]
    public class Weather
    {
        public string description;
    }

    void Start()
    {
        apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
        StartCoroutine(GetWeatherData());
    }

    IEnumerator GetWeatherData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Parse the JSON data
            WeatherData weatherData = JsonUtility.FromJson<WeatherData>(request.downloadHandler.text);
            //float temperature = weatherData.main.temp;
            int temperature = Mathf.RoundToInt(weatherData.main.temp); // Round the temperature to the nearest whole number
            //string weatherDescription = weatherData.weather[0].description;
            string weatherDescription = CapitalizeFirstLetter(weatherData.weather[0].description);

            // Update UI
            temperatureText.text = $"{temperature}°C";
            weatherDescriptionText.text = weatherDescription;
        }
    }

    private string CapitalizeFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
