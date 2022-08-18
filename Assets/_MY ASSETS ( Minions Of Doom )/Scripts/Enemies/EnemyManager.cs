using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// This class keeps track of all enemies.
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public List <EnemyBase> enemies;

    private Vector3 m_RandomEnemyPos;
    private EnemyBase m_RandomEnemy;
    
    private void Start()
    {
        GameManager.Instance.TurnManager.EnemyTurn += OnEnemyTurn;
    }

    /// <summary>
    /// This is called when a new enemy is spawned
    /// </summary>
    public void OnEnemySpawned(EnemyBase enemy)
    {
        enemies.Add(enemy);
        enemy.Dead += CheckRemainingEnemyCount;        
        
        m_RandomEnemy = GetRandomEnemy();
        m_RandomEnemyPos = m_RandomEnemy.transform.position;
    }

    
    private void CheckRemainingEnemyCount()
    {
        if (enemies.Count == 0)
        {
            GameManager.Instance.RewardManager.SpawnRewardPrefab(m_RandomEnemyPos,m_RandomEnemy.EnemyData.enemyType);
        }
    }
    
    /// <summary>
    /// This is called when the enemies turn begins.
    /// </summary>
    private void OnEnemyTurn()
    {
        StartCoroutine(AllEnemiesPlayingTurn());
    }

    /// <summary>
    /// Coroutine that triggers the enemies turn one after each other.
    /// </summary>
    /// <returns></returns>
    IEnumerator AllEnemiesPlayingTurn()
    {
        foreach (var enemy in enemies)
        {            
            enemy.OnEnemyTurn();
            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// Returns a random enemy from the enemies list
    /// </summary>
    /// <returns></returns>
    public EnemyBase GetRandomEnemy()
    {
        return enemies[Random. Range(0, enemies.Count)];;
    }
}