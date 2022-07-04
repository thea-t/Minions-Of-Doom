using System.Collections.Generic;
using UnityEngine;

//Level Data used by the map (See Map.cs) to create easily different levels
//The idea of this class is to store different door data and pick random doors based on a given door count which will be instantiated by the map

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    private const int DOOR_COUNT = 3;
    
    public DoorData[] doorData;
    [HideInInspector] public int doorCount { get; private set; }

    public List<DoorData> RandomDoors { get; private set; } = new List<DoorData>();

    private void Start()
    {
        doorCount = DOOR_COUNT;
    }
    
    public void PickRandomDoors() { 
        RandomDoors.Clear();
        
        for (int i = 0; i < doorCount; i++) {

            int rand = Random.Range(0, doorData.Length);
            RandomDoors.Add(doorData[rand]);
        }
    }

}

