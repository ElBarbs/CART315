using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class MeterManager : MonoBehaviour
{
    public GameObject musicMeterGameObject;
    public GameObject trashMeterGameObject;
    public GameObject drinkMeterGameObject;

    // Example method to update a specific meter
    public void UpdateSpecificMeter(string meterName, float fillAmount)
    {
        fillAmount = Mathf.Clamp(fillAmount, 0f, 1f); // Ensure fillAmount is valid

        switch (meterName)
        {
            case "Music":
                musicMeterGameObject.GetComponent<Image>().fillAmount = fillAmount;
                break;
            case "Trash":
                trashMeterGameObject.GetComponent<Image>().fillAmount = fillAmount;
                break;
            case "Drink":
                drinkMeterGameObject.GetComponent<Image>().fillAmount = fillAmount;
                break;
            default:
                Debug.LogWarning("Meter name not recognized.");
                break;
        }
    }
}
