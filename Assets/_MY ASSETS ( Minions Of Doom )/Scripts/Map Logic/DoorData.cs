using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used for spawning different levels depending on the door data (See LevelData.cs)
/// </summary>
[CreateAssetMenu]
public class DoorData : ScriptableObject
{
    public Door doorPrefab;
    public GameObject objectToSpawn;
    public bool isCandleRoom;
}
