using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

public class Map : MonoBehaviour 
{
    
    [SerializeField] private PlayerVR m_Player;
    [SerializeField] private LevelData[] levels;
    
    private List<Door> m_SpawnedDoors = new List<Door>();
    
    private const float DOOR_DISTANCE = 1f;

    /// <summary>
    /// Spawning random doors based on the current level when the game starts
    /// Setting their position and distance from each other
    /// </summary>
    private void Start()
    {
        int duration = 8; 
        SpawnDoors(levels[Player.CurrentLevel].doorCount);
        m_Player.EnterScene(new Vector3(m_Player.transform.position.x, 1.3f, -3f), duration);
        StartCoroutine(AllowPlayerToInteractWithDoors(duration));
    }

    /// <summary>
    /// Waits some time and lets the player interact with doors.
    /// </summary>
    IEnumerator AllowPlayerToInteractWithDoors(int duration)
    {
        yield return new WaitForSeconds(duration+1);
        m_Player.AllowPlayerToInteractWithDoors();
    }

    /// <summary>
    /// Picks random doors from the current level and instantiates them.
    /// </summary>
    private void SpawnDoors(int _doorCount)
    {
        m_SpawnedDoors.Clear();
        
        for (int i = 0; i < _doorCount; i++)
        {
            levels[Player.CurrentLevel].PickRandomDoors();
        }

        for (int i = 0; i < levels[Player.CurrentLevel].RandomDoors.Count; i++)
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

            var door = Instantiate(levels[Player.CurrentLevel].RandomDoors[i].doorPrefab, pos, Quaternion.identity);
            m_SpawnedDoors.Add(door);
            
            //saving the selected door in a static class in order to be able to launch a level based on the selected door
            Player.SelectedDoor = levels[Player.CurrentLevel].RandomDoors[i]; 
            
            door.gameObject.name = levels[Player.CurrentLevel].RandomDoors[i].name;
        }
    }

}
