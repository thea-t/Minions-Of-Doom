using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private Chest[] m_Chests;

    //spawn random chest at the position of a randomly picked enemy
    public void SpawnRewardPrefab(Vector3 pos, EnemyType enemyType)
    {
        Chest randomChest = m_Chests[Random.Range(0, m_Chests.Length)];

        randomChest.EnemyType = enemyType;
        
        Instantiate(randomChest, pos, Quaternion.Euler(new Vector3(270,180,0)));
    }
}
