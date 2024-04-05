using System;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.SceneManagement;

public class TimeProgression : MonoBehaviour
{
    public TMP_Text timeText; // Reference to the TextMeshPro text component
    public float durationInMinutes = 10; // Duration from 20:00 to 6:00 in minutes

    private float startTime;
    private TimeSpan startHour = new TimeSpan(20, 0, 0); // Starting time 20:00
    private TimeSpan endHour = new TimeSpan(6, 0, 0); // Ending time 6:00
    private TimeSpan totalDuration;




    void Start()
    {
        // Calculate the total duration required to go from 20:00 to 6:00
        if (endHour > startHour)
            totalDuration = endHour.Subtract(startHour);
        else
            totalDuration = (TimeSpan.FromHours(24) - startHour).Add(endHour);

        startTime = Time.time; // Record the start time
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime; // Calculate elapsed time in seconds
        float progress = elapsedTime / (durationInMinutes * 60); // Progress as a fraction of the total duration

        // Calculate the current time of day based on progress
        TimeSpan currentTimeOfDay = startHour.Add(TimeSpan.FromTicks((long)(totalDuration.Ticks * progress)));

        // Adjust for looping
        if (currentTimeOfDay >= TimeSpan.FromHours(24))
            currentTimeOfDay = currentTimeOfDay.Subtract(TimeSpan.FromHours(24));

        // Update the TextMeshPro text to show the current time
        timeText.text = currentTimeOfDay.ToString(@"hh\:mm");

        // Loop the time progression
        if (progress >= 1)
        {
            SceneManager.LoadScene("EndGame");
        }
    }
}
