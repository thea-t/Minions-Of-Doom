 using System;
using System.Collections;
using System.Collections.Generic;
 using BNG;
 using UnityEngine;
 using Random = UnityEngine.Random;


 public class LevelLauncher : MonoBehaviour
{
 void Start() 
 {
   var obj = Instantiate(Player.SelectedDoor.objectToSpawn);
 }

 
 
}
