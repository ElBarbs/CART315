using System;
using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class MeterManager : MonoBehaviour
{
    private Image _happinessMeter, _drinkMeter, _trashMeter, _musicMeter;

    private void Start()
    {
        _happinessMeter = GameObject.FindWithTag("HappinessMeter").GetComponent<Image>();
        _drinkMeter = GameObject.FindWithTag("DrinkMeter").GetComponent<Image>();
        _trashMeter = GameObject.FindWithTag("TrashMeter").GetComponent<Image>();
        _musicMeter = GameObject.FindWithTag("MusicMeter").GetComponent<Image>();
    }

    // Example method to update a specific meter
    public void UpdateSpecificMeter(string meterName, float fillAmount)
    {
        switch (meterName)
        {
            case "Happiness":
                _happinessMeter.fillAmount += fillAmount;
                break;
            case "Drink":
                _drinkMeter.fillAmount += fillAmount;
                break;
            case "Trash":
                _trashMeter.fillAmount += fillAmount;
                break;
            case "Music":
                _musicMeter.fillAmount += fillAmount;
                break;
            default:
                Debug.LogWarning("Meter name not recognized.");
                break;
        }
    }

    // New method to get the current fillAmount of a meter
    public float GetFillAmount(string meterName)
    {
        switch (meterName)
        {
            case "Happiness":
                return _happinessMeter.fillAmount;
            case "Drink":
                return _drinkMeter.fillAmount;
            case "Trash":
                return _trashMeter.fillAmount;
            case "Music":
                return _musicMeter.fillAmount;
            default:
                Debug.LogWarning("Meter name not recognized.");
                return 0f; // Return 0 if meter name is unrecognized
        }
    }

    public void UpdateHappinessMeter(float happinessLevel)
    {
        // Assuming happinessLevel is a value between 0 and 1 representing the percentage of happiness
        _happinessMeter.fillAmount = happinessLevel;
    }
}
