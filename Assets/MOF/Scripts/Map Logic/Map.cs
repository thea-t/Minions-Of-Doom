using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class Map : MonoBehaviour 
{
    private const float DOOR_DISTANCE = 0.75f;
    
    [SerializeField] private FloorData[] floors;
    
    private int currentFloor = 0;

    //on floor climbed = current floor++;

    private void Start() {
        SpawnDoors(floors[currentFloor].doorCount);
    }

    public List<Door> spawnedDoors = new List<Door>();
    private void SpawnDoors(int _doorCount)
    {
        spawnedDoors.Clear();
        
        for (int i = 0; i < _doorCount; i++)
        {
            floors[currentFloor].PickRandomDoors();
        }

        for (int i = 0; i < floors[currentFloor].RandomDoors.Count; i++)
        {
            Vector3 pos = Vector3.zero;

            //pos.x = i % 2 == 0 ? i * 1.5f : i * (-1.5f);
            if (i % 2 == 0)
            {
                pos.x = i * DOOR_DISTANCE;
            }
            else
            {
                pos.x = (i * -DOOR_DISTANCE) - DOOR_DISTANCE;
            }

            var door = Instantiate(floors[currentFloor].RandomDoors[i].doorPrefab, pos, Quaternion.identity);
            spawnedDoors.Add(door);
            door.gameObject.name = floors[currentFloor].RandomDoors[i].name;
            Player.SelectedDoor = floors[currentFloor].RandomDoors[i];
        }
    }

}
