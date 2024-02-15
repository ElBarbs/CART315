using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutGameManager : MonoBehaviour
{
    public int lives, points;

    public static BreakoutGameManager Instance { get; private set; }
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
    
    void Start()
    {
        lives = 3;
        points = 0;
    }
    
    void Update()
    {
        
    }
}
