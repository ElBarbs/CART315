using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public GameObject brick;
    public int rows, cols;
    public float brickSpacingRow = 0.5f, brickSpacingCol = 1f;
    
    void Start()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject newBrick = Instantiate(brick, new Vector3(-cols + (i * brickSpacingCol) + 3.5f, rows - (j * brickSpacingRow)), Quaternion.identity, transform);
                newBrick.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
        }
        
    }
    
    void Update()
    {
        
    }
}
