using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    [SerializeField] private FloorItem floorItem;
    [SerializeField] private DoorHelper doorHelper;

    private void Reset()
    {
        doorHelper = GetComponentInChildren<DoorHelper>();
    }

    void Start()
    {
        doorHelper.DoorOpened += OnDoorOpened;
    }
    
    private void OnDoorOpened(DoorHelper _door)
    {
        Player.FloorItem = floorItem;
        
    }
}
