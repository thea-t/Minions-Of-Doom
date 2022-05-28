 using System;
using System.Collections;
using System.Collections.Generic;
 using BNG;
 using UnityEngine;
 using Random = UnityEngine.Random;

//Launches a level by instantiating a prefab stored in the opened by the player door
 public class LevelLauncher : MonoBehaviour
 {
  [SerializeField] private DoorData debugDoor;
  void Awake()
  {
   if (!Player.SelectedDoor) { Player.SelectedDoor = debugDoor; Debug.Log("!!!DOOR NOT SELECTED!!!"); }
   
   GameObject obj = Instantiate(Player.SelectedDoor.objectToSpawn);
  }
 }
