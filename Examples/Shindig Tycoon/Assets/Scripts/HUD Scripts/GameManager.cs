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
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        //Create new Meters with a starting value of 100
        MusicMeter = new BaseMeter(100f);
        TrashMeter = new BaseMeter(100f);
        DrinkMeter = new BaseMeter(100f);

    }


    void PanelUpdate()
    {
        PanelManager.GetComponent<MeterManager>().UpdateSpecificMeter("Drink", -0.1f);
        PanelManager.GetComponent<MeterManager>().UpdateSpecificMeter("Music", -0.1f);
        PanelManager.GetComponent<MeterManager>().UpdateSpecificMeter("Trash", -0.1f);

    }

    public void UpdateMeter(string meterName, float fillAmount)
    {
        PanelManager.GetComponent<MeterManager>().UpdateSpecificMeter(meterName, fillAmount);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     PanelUpdate();
        // }

        if (CalculateOverallHappiness() <= 0)
        {
            SceneManager.LoadScene("EndGame");
        }


    }

    public float CalculateOverallHappiness()
    {
        // Calculate the sum of the current values
        float currentValueSum = MusicMeter.CurrentValue + TrashMeter.CurrentValue + DrinkMeter.CurrentValue;

        // Scale the sum to the overall happiness range
        float scaledHappiness = (currentValueSum / TotalMaxValue) * HappinessMaxValue;

        // Ensure overall happiness does not exceed the max value
        scaledHappiness = Mathf.Clamp(scaledHappiness, 0, HappinessMaxValue);

        return scaledHappiness;
    }


}
