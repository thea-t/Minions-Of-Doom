using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorData : ScriptableObject
{
    public FloorItemBase[] floorItems;
    public int doorCount;

    public List<FloorItemBase> randomItems = new List<FloorItemBase>();

    
    public void PickRandomDoors() { 
        randomItems.Clear();
        
        for (int i = 0; i < doorCount; i++) {

            int rand = Random.Range(0, floorItems.Length);
            randomItems.Add(floorItems[rand]);
        }
    }

}

