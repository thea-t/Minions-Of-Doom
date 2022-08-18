using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private Chest[] m_Chests;

    /// <summary>
    /// Spawns random chest at the position of a randomly picked enemy
    /// </summary>
    public void SpawnRewardPrefab(Vector3 pos, EnemyType enemyType)
    {
        Chest randomChest = m_Chests[Random.Range(0, m_Chests.Length)];
        
        randomChest.SetEnemyType(enemyType);
        
        Instantiate(randomChest, pos, Quaternion.Euler(new Vector3(270,180,0)));
    }
}
