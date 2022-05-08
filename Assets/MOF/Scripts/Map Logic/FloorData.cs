using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorData : ScriptableObject
{
    public DoorData[] doorData;
    public int doorCount;

    public List<DoorData> RandomDoors { get; private set; } = new List<DoorData>();


    public void PickRandomDoors() { 
        RandomDoors.Clear();
        
        for (int i = 0; i < doorCount; i++) {

            int rand = Random.Range(0, doorData.Length);
            RandomDoors.Add(doorData[rand]);
        }
    }

}

