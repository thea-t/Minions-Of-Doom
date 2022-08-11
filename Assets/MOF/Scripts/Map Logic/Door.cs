using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class checks if a door is open and loads the Game Scene 
public class Door : MonoBehaviour
{
    [SerializeField] private DoorHelper doorHelper; // DoorHelper.cs comes from the VR Interaction Framework asset 

    private void Reset()
    {
        doorHelper = GetComponentInChildren<DoorHelper>();
    }

    private void Start() {
        doorHelper.DoorOpened += OnDoorOpened;
    }
    private void OnDoorOpened()
    {
       SceneLoader.FadeToScene("GameScene");
    }
    
}
