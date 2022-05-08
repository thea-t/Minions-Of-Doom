using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    
   // [SerializeField] private FloorItem floorItem;
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
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
