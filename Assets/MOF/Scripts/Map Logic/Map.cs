using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    [SerializeField] private FloorData[] floors;
    
    private int currentFloor = 0;

    //on floor climbed = current floor++;

    private void Start() {
        SpawnDoors(floors[currentFloor].doorCount);
    }

    private void SpawnDoors(int _doorCount) {
        for (int i = 0; i < _doorCount; i++)
        {
         floors[currentFloor].PickRandomDoors();   
        }

        foreach (var item in floors[currentFloor].randomItems)
        {
            Instantiate(item.doorPrefab);
        }
    }
}
