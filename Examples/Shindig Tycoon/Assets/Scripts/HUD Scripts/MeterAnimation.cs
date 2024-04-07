using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class MeterManager : MonoBehaviour
{
    public GameObject HappMeter;
    public GameObject DrinkMeter;
    public GameObject TrashMeter;
    public GameObject MusicMeter;


    // Example method to update a specific meter
    public void UpdateSpecificMeter(string meterName, float fillAmount)
    {
        switch (meterName)
        {
            case "Happ":
                HappMeter.GetComponent<Image>().fillAmount += fillAmount;
                break;
            case "Drink":
                DrinkMeter.GetComponent<Image>().fillAmount += fillAmount;
                break;
            case "Trash":
                TrashMeter.GetComponent<Image>().fillAmount += fillAmount;
                break;
            case "Music":
                MusicMeter.GetComponent<Image>().fillAmount += fillAmount;
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
            case "Happ":
                return HappMeter.GetComponent<Image>().fillAmount;
            case "Drink":
                return DrinkMeter.GetComponent<Image>().fillAmount;
            case "Trash":
                return TrashMeter.GetComponent<Image>().fillAmount;
            case "Music":
                return MusicMeter.GetComponent<Image>().fillAmount;


            default:
                Debug.LogWarning("Meter name not recognized.");
                return 0; // Return 0 if meter name is unrecognized
        }


    }

    public void UpdateHappinessMeter(float happinessLevel)
    {
        // Assuming happinessLevel is a value between 0 and 1 representing the percentage of happiness
        HappMeter.GetComponent<Image>().fillAmount = happinessLevel;
    }
}
