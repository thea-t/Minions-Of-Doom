using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

public class Map : MonoBehaviour 
{
    private const float DOOR_DISTANCE = 1f;
    
    [SerializeField] private GameObject m_Player;
    [SerializeField] private LevelData[] levels;
    private List<Door> m_SpawnedDoors = new List<Door>();
    
    private int currentLevel = 0;
    

    
    //Spawning random doors based on the current level when the game starts
    //Setting their position and distance from each other
    private void Start() 
    {
        SpawnDoors(levels[currentLevel].doorCount);
        
        m_Player.transform.DOMove(new Vector3(m_Player.transform.position.x, 0.7f, -3f), 8).onComplete = () => 
        {
            m_Player.GetComponentInChildren<PlayerGravity>().GravityEnabled = true;
            m_Player.GetComponent<InputBridge>().enabled = true;
        };
    }

    private void SpawnDoors(int _doorCount)
    {
        m_SpawnedDoors.Clear();
        
        for (int i = 0; i < _doorCount; i++)
        {
            levels[currentLevel].PickRandomDoors();
        }

        for (int i = 0; i < levels[currentLevel].RandomDoors.Count; i++)
        {
            Vector3 pos = Vector3.zero;

            //checking if to spawn the door on the right or on the left side based on if the door count is odd or even number
            //pos.x = i % 2 == 0 ? i * 1.5f : i * (-1.5f); // short way of writing it 
            if (i % 2 == 0)
            {
                pos.x = i * DOOR_DISTANCE;
            }
            else
            {
                pos.x = (i * -DOOR_DISTANCE) - DOOR_DISTANCE;
            }

            var door = Instantiate(levels[currentLevel].RandomDoors[i].doorPrefab, pos, Quaternion.identity);
            m_SpawnedDoors.Add(door);
            
            //saving the selected door in a static class in order to be able to launch a level based on the selected door
            Player.SelectedDoor = levels[currentLevel].RandomDoors[i]; 
            
            door.gameObject.name = levels[currentLevel].RandomDoors[i].name;
            Debug.Log("door: " + door.gameObject.name);
        }
    }

}
