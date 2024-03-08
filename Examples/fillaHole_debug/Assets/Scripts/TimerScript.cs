using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private Text _timerText;
    private float _levelTime;

    // Start is called before the first frame update
    private void Start()
    {
        _timerText = GetComponent<Text>();
        ResetTimer();
    }

    private void ResetTimer()
    {
        _levelTime = 30;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_levelTime > 0)
        {
            _levelTime -= Time.deltaTime;
            _timerText.text = Math.Round(_levelTime, 1) + " SECONDS LEFT"; 
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}