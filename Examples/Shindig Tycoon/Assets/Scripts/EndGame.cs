using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Button playAgainButton; // Reference to the Play Again button
    public string sceneToLoad; // Name of the scene to load when Play Again is clicked

    // Start is called before the first frame update
    void Start()
    {
        // Find the Play Again button in the scene

        // Add a click listener to the Play Again button
        playAgainButton.onClick.AddListener(PlayAgain);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to be called when Play Again is clicked
    void PlayAgain()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}