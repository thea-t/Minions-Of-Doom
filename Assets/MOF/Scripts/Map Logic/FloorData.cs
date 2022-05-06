using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorData : ScriptableObject
{
    public FloorItemBase[] floorItems;
    public int doorCount;

    private List<FloorItemBase> m_RandomItems;

    public void PickRandomDoors() {
        for (int i = 0; i < doorCount; i++) {

            int rand = Random.Range(0, floorItems.Length);
            m_RandomItems.Add(floorItems[rand]);
        }
    }

}

