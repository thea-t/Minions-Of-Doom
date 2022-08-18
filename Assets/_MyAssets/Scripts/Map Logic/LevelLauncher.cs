using UnityEngine;

/// <summary>
/// Launches a level by instantiating a prefab stored in the opened by the player door
/// </summary>
public class LevelLauncher : MonoBehaviour
{
    [SerializeField] private DoorData debugDoor;
    [SerializeField] private bool debug;

    void Awake()
    {
        if (debug && !Player.SelectedDoor)
        {
            Player.SelectedDoor = debugDoor;
        }

        GameObject obj = Instantiate(Player.SelectedDoor.objectToSpawn);
    }
}