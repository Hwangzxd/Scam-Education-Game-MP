using UnityEngine;
using UnityEngine.UI;

public class ReputationBar : MonoBehaviour
{
    // gets the Slider component and calls it slider
    public Slider slider;

    void Start()
    {
        // sets the max reputation value to 100 and also sets it to 100
        SetMaxReputation(100);
    }

    void Update()
    {
        // debugging purposes
        if (Input.GetKeyDown(KeyCode.A))
        {
            MinusReputation(10);
            Debug.Log("minus");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlusReputation(10);
            Debug.Log("plus");
        }
    }

    // sets the reputation bar value to desired value
    public void SetReputationValue(int value)
    {
        // changes the slider's current value
        slider.value = value;
    }

    // sets max value of reputation bar and also the current value
    public void SetMaxReputation(int value)
    {
        //changes the slider's max value
        slider.maxValue = value;

        //changes the slider's current value
        slider.value = value;
    }

    // takes away from the reputation bar
    public void MinusReputation(int damage)
    {
        slider.value -= damage;
    }

    public void PlusReputation(int value)
    {
        slider.value += value;
    }

    private void OnReputationDrained()
    {
        Debug.Log("Reputation is completely drained.");
    }

    //to get the value of reputation
    public float GetReputation()
    {
        return slider.value;
    }

}
