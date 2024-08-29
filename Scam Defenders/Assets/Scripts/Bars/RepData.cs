using UnityEngine;
using System;

public class RepData : MonoBehaviour
{
    public static RepData Instance;

    public int repValue;

    public int initialRep = 50;
    public int maxRep = 100;
    public int minRep = 0;

    public event Action RepValueChanged; // Event to notify when reputation changes

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this object persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        // Initialize the reputation value
        repValue = initialRep;
        RepValueChanged?.Invoke(); // Invoke the event to update the UI
    }

    public void SetReputationValue(int value)
    {
        repValue = Mathf.Clamp(value, minRep, maxRep);

        RepValueChanged?.Invoke(); // Invoke the event to update the UI

        // Check if reputation is completely drained
        if (repValue == minRep)
        {
            OnReputationDrained();
        }
    }

    public void MinusReputation(int damage)
    {
        SetReputationValue((int)repValue - damage);
    }

    public void PlusReputation(int value)
    {
        SetReputationValue((int)repValue + value);
    }

    private void OnReputationDrained()
    {
        Debug.Log("Reputation is completely drained.");
        // Implement additional logic when reputation is fully drained, if needed
    }

    public float GetReputation()
    {
        return repValue;
    }
}
