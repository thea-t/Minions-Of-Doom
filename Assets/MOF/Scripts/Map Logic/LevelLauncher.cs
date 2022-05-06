 using System;
using System.Collections;
using System.Collections.Generic;
 using BNG;
 using UnityEngine;


public class LevelLauncher : MonoBehaviour
{
    public FloorData floor;
    public DoorHelper door;

    private void LaunchLevel(FloorData floorData)
    {
        LaunchLevel(floor);
    }
}
