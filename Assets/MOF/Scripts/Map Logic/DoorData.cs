using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Used for spawning different levels depending on the door data (See LevelData.cs)
[CreateAssetMenu]
public class DoorData : ScriptableObject {
    public Door doorPrefab;
    public GameObject objectToSpawn;
}
