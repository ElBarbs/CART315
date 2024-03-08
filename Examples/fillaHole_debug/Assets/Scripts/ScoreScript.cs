using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    private Text _scoreText;
    private int _score;

    public static ScoreScript S;
    
    // Start is called before the first frame update
    void Start() {
        _score = 0;
        _scoreText = GetComponent<Text>();
        DisplayScore();
        S = this;
    }

    public void IncreaseScore() {
        _score++;
        DisplayScore();
    }
    
    public void DecreaseScore() {
        _score--;
        DisplayScore();
    }

    private void DisplayScore() {
        _scoreText.text = "SCORE: " + _score;
    }
}
