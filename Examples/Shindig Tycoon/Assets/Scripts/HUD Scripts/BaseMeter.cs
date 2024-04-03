using UnityEngine;


public class BaseMeter
{
    public float CurrentValue { get; private set; }
    private float maxValue = 100f; // Assuming 100 is the max value of the meter

    // Constructor
    public BaseMeter(float initialValue)
    {
        CurrentValue = initialValue;
    }

    // Method to update the meter value
    public void UpdateValue(float valueChange, float weight)
    {
        // Calculate the weighted change
        float weightedChange = valueChange * weight;

        // Update the current value with the weighted change
        // Ensure the value stays within 0 and maxValue
        CurrentValue = Mathf.Clamp(CurrentValue + weightedChange, 0, maxValue);
    }
}
