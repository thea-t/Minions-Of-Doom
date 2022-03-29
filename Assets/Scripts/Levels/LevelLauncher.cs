using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launch picked level from the map.
/// </summary>
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
