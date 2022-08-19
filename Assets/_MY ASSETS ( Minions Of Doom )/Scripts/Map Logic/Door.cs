using BNG;
using UnityEngine;

/// <summary>
/// This class checks if a door is opened and loads the Game Scene 
/// </summary>
public class Door : MonoBehaviour
{
    // DoorHelper.cs comes from the VR Interaction Framework asset
    [SerializeField] private DoorHelper doorHelper;  

    private void Reset()
    {
        doorHelper = GetComponentInChildren<DoorHelper>();
    }

    private void Start() {
        doorHelper.DoorOpened += OnDoorOpened;
    }
    
    private void OnDoorOpened()
    {
        if (Player.SelectedDoor.isCandleRoom) {
            StartCoroutine(SceneLoader.FadeToScene("CandleRoom"));
        }
        else {
            StartCoroutine(SceneLoader.FadeToScene("GameScene"));
        }
    }
    
}
