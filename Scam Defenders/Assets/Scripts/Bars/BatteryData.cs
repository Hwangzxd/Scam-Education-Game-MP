using UnityEngine;

public class BatteryData : MonoBehaviour
{
    // Battery value
    private float batteryValue = 100f;

    // Duration for battery drain (5 minutes)
    private float totalTime = 300f;
    private float elapsedTime = 0f;

    // Singleton instance
    public static BatteryData Instance { get; private set; }

    void Awake()
    {
        // Ensure only one instance of BatteryData exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Update battery level over time
        if (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            batteryValue = Mathf.Lerp(100, 0, elapsedTime / totalTime);
        }
        else
        {
            batteryValue = 0;
        }
    }

    // Method to get the current battery value
    public float GetBatteryValue()
    {
        return batteryValue;
    }
}
