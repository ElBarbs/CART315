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
                Debug.Log(DrinkMeter.GetComponent<Image>().fillAmount);
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
}
