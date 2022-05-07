 using System;
using System.Collections;
using System.Collections.Generic;
 using BNG;
 using UnityEngine;


public class LevelLauncher : MonoBehaviour
{
    [HideInInspector] public FloorData floor;
    [HideInInspector] public DoorHelper door;

    private void Start()
    {
        LaunchLevel(floor);
    }

    private void LaunchLevel(FloorData floorData)
    {
        
    }
}
