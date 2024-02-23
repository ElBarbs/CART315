using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject scoreGameObject, healthGameObject;
    public GameObject enemyPrefab;
    public float enemySpawnInterval = 1.5f;

    private readonly Color[] COLORS = { new Color(0, 0, 1f), new Color(0, 1f, 0), new Color(1f, 0, 0) };
    private GameObject _player;
    private TextMeshProUGUI _scoreText, _healthText;
    private int _score;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _score = 0;
        _scoreText = scoreGameObject.GetComponent<TextMeshProUGUI>();
        _healthText = healthGameObject.GetComponent<TextMeshProUGUI>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
        // Start spawning enemies.
        StartCoroutine(nameof(SpawnEnemyRoutine));
    }

    private void SpawnEnemy()
    {
        var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        SpriteRenderer enemyRenderer = newEnemy.GetComponent<SpriteRenderer>();
        newEnemy.GetComponent<SpriteRenderer>().color = COLORS[Random.Range(0, COLORS.Length)];
    }

    IEnumerator SpawnEnemyRoutine()
    {
        // Infinite loop to continuously spawn enemies.
        while (true)
        {   
            // Calculate a random position within the screen boundaries.
            var randomPosition = new Vector3(
                Random.Range(-10.5f, 10.5f),
                Random.Range(-5f, 5f)
            );
            
            // Instantiate an enemy at the random position and randomize its color.
            var newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            newEnemy.GetComponent<SpriteRenderer>().color = COLORS[Random.Range(0, COLORS.Length)];

            // Wait for the specified spawn interval.
            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }

    public void IncreaseScore(int value)
    {
        _score += value;
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateHealthUI(int value)
    {
        _healthText.text = value.ToString();
    }

    public void EndGame()
    {
        SceneManager.LoadSceneAsync("EndScreen");
    }
}