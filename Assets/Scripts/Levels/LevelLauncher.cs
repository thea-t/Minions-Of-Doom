using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Launch picked level from the map.

public class LevelLauncher : MonoBehaviour
{
    [SerializeField] private LevelData testLevel;
    private void Start()
    {
        LaunchLevel(testLevel);
    }

    private void LaunchLevel(LevelData levelData)
    {
        
    }
}
