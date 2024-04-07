using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BaseMeter MusicMeter { get; private set; }
    public BaseMeter TrashMeter { get; private set; }
    public BaseMeter DrinkMeter { get; private set; }

    public GameObject PanelManager;
    private const float TotalMaxValue = 300f; // Sum of max values of all meters
    private const float HappinessMaxValue = 200f; // Max value for overall happiness

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // Create new Meters with a starting value of 100
        MusicMeter = new BaseMeter(100f);
        TrashMeter = new BaseMeter(100f);
        DrinkMeter = new BaseMeter(100f);

        // Initial UI Update
        UpdateMeterUI("Music");
        UpdateMeterUI("Trash");
        UpdateMeterUI("Drink");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Example of updating a meter
            UpdateMeter("Trash", -10f);
        }

        if (CalculateOverallHappinessAndUpdateUI() <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void UpdateMeter(string meterName, float fillAmount)
    {
        // Assuming fillAmount is a change in value, not the new fill amount
        // and assuming a weight of 1 for simplicity; adjust as needed
        float weight = 1; // Default weight, adjust as per your game's logic

        switch (meterName)
        {
            case "Trash":
                TrashMeter.UpdateValue(fillAmount, weight);
                break;
            case "Drink":
                DrinkMeter.UpdateValue(fillAmount, weight);
                break;
            case "Music":
                MusicMeter.UpdateValue(fillAmount, weight);
                break;

        }

        // Now reflect this change in the UI
        UpdateMeterUI(meterName);
        CalculateOverallHappinessAndUpdateUI();



    }


    void UpdateMeterUI(string meterName)
    {
        float fillAmount = 0f;
        switch (meterName)
        {
            case "Trash":
                fillAmount = TrashMeter.CurrentValue / TrashMeter.MaxValue; // Assuming MaxValue is defined
                break;
            case "Drink":
                fillAmount = DrinkMeter.CurrentValue / DrinkMeter.MaxValue;
                break;
            case "Music":
                fillAmount = MusicMeter.CurrentValue / MusicMeter.MaxValue;
                break;
                // Add cases for other meters as necessary
        }

        // Calculate how much to adjust the UI meter
        float uiAdjustment = fillAmount - PanelManager.GetComponent<MeterManager>().GetFillAmount(meterName);
        PanelManager.GetComponent<MeterManager>().UpdateSpecificMeter(meterName, uiAdjustment);
    }

    public float CalculateOverallHappinessAndUpdateUI()
    {
        // Existing logic to calculate overall happiness
        float currentValueSum = TrashMeter.CurrentValue;
        float scaledHappiness = (currentValueSum / TotalMaxValue) * HappinessMaxValue;
        scaledHappiness = Mathf.Clamp(scaledHappiness, 0, HappinessMaxValue);

        // Normalize the happiness value to a 0-1 scale for the UI
        float normalizedHappiness = scaledHappiness / HappinessMaxValue;

        // Update the happiness meter UI
        PanelManager.GetComponent<MeterManager>().UpdateHappinessMeter(normalizedHappiness);

        Debug.Log("Overall Happiness: " + scaledHappiness);
        return scaledHappiness;
    }

}
