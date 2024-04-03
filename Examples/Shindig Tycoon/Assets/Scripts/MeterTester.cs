using UnityEngine;

public class MeterTester : MonoBehaviour
{
    public enum MeterType { Music, Trash, Drink }
    private GameManager gameManager;

    public MeterType meterToUpdate; // Allows selection in Unity Editor
    public float updateValue = 10f; // Default value for the update
    public float weight = 1f; // Default weight

    void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateAndLogMeter();
        }


    }

    void UpdateAndLogMeter()
    {
        if (gameManager == null) return;

        BaseMeter selectedMeter = null;

        // Select the appropriate meter
        switch (meterToUpdate)
        {
            case MeterType.Music:
                selectedMeter = gameManager.MusicMeter;
                break;
            case MeterType.Trash:
                selectedMeter = gameManager.TrashMeter;
                break;
            case MeterType.Drink:
                selectedMeter = gameManager.DrinkMeter;
                break;
        }

        // Update the selected meter
        if (selectedMeter != null)
        {
            selectedMeter.UpdateValue(updateValue, weight);
            Debug.Log($"{meterToUpdate} Meter: {selectedMeter.CurrentValue}");

            // Log overall happiness
            float overallHappiness = gameManager.CalculateOverallHappiness();
            Debug.Log($"Overall Happiness: {overallHappiness}");
        }
    }
}
