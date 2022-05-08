using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorData : ScriptableObject
{
    public Door[] doors;
    public int doorCount;

    public List<Door> randomDoors = new List<Door>();

    
    public void PickRandomDoors() { 
        randomDoors.Clear();
        
        for (int i = 0; i < doorCount; i++) {

            int rand = Random.Range(0, doors.Length);
            randomDoors.Add(doors[rand]);
        }
    }

}

