using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public BaseMeter MusicMeter { get; private set; }
    public BaseMeter TrashMeter { get; private set; }
    public BaseMeter DrinkMeter { get; private set; }
    
    private Image _happinessMeter, _drinkMeter, _trashMeter, _musicMeter;
    
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
        _happinessMeter = GameObject.FindWithTag("HappinessMeter").GetComponent<Image>();
        _drinkMeter = GameObject.FindWithTag("DrinkMeter").GetComponent<Image>();
        _trashMeter = GameObject.FindWithTag("TrashMeter").GetComponent<Image>();
        _musicMeter = GameObject.FindWithTag("MusicMeter").GetComponent<Image>();
        
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
        UpdateSpecificMeter(meterName, fillAmount - GetFillAmount(meterName));
    }

    private float CalculateOverallHappinessAndUpdateUI()
    {
        // Existing logic to calculate overall happiness
        float currentValueSum = TrashMeter.CurrentValue;
        float scaledHappiness = (currentValueSum / TotalMaxValue) * HappinessMaxValue;
        scaledHappiness = Mathf.Clamp(scaledHappiness, 0, HappinessMaxValue);

        // Normalize the happiness value to a 0-1 scale for the UI
        float normalizedHappiness = scaledHappiness / HappinessMaxValue;

        // Set the happiness meter UI
        SetHappinessMeter(normalizedHappiness);
        
        return scaledHappiness;
    }
    
    private void UpdateSpecificMeter(string meterName, float fillAmount)
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
    private float GetFillAmount(string meterName)
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

    private void SetHappinessMeter(float happinessLevel)
    {
        // Assuming happinessLevel is a value between 0 and 1 representing the percentage of happiness
        _happinessMeter.fillAmount = happinessLevel;
    }

}
