 using System;
using System.Collections;
using System.Collections.Generic;
 using BNG;
 using UnityEngine;
 using Random = UnityEngine.Random;


 public class LevelLauncher : MonoBehaviour
{
 [SerializeField] private GameObject[] beastLevels;
 [SerializeField] private  GameObject[] bossLevels;
 [SerializeField] private  GameObject[] candleLevels;
 [SerializeField] private  GameObject[] chestLevels;
 [SerializeField] private  GameObject[] letterLevels;
 [SerializeField] private  GameObject[] monsterLevels;
 [SerializeField] private  GameObject[] shopLevels;
 [SerializeField] private GameObject[] MiniGameLevels;
 
 private void SpawnRandomLevel(FloorItem floorItem)
 {
  GameObject currentLevel;
  
  switch (floorItem)
  {
   case FloorItem.Beast:
    int randBeast = Random.Range(0, beastLevels.Length);
    currentLevel = beastLevels[randBeast];
    break;
   
   case FloorItem.Boss:
    int randBoss = Random.Range(0, bossLevels.Length);
    currentLevel = bossLevels[randBoss];
    break;
   
   case FloorItem.Candle:
    
    break;
   
   case FloorItem.Chest:
    
    break;
   case FloorItem.Letter:
    
    break;
   
   case FloorItem.Monster:
    
    break;
   case FloorItem.Shop:
    
    break;
   
   case FloorItem.MiniGame:
    
    break;
  }
 }
}
