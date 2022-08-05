using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public List <EnemyBase> enemies;

    private Vector3 m_RandomEnemyPos;
    private EnemyBase m_RandomEnemy;
    private void Start()
    {
        GameManager.Instance.TurnManager.EnemyTurn += OnEnemyTurn;
    }

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
    private void OnEnemyTurn()
    {
        StartCoroutine(AllEnemiesPlayingTurn());
    }

    IEnumerator AllEnemiesPlayingTurn()
    {
        foreach (var enemy in enemies)
        {            
            enemy.OnEnemyTurn();
            yield return new WaitForSeconds(1);
        }
    }

    public EnemyBase GetRandomEnemy()
    {
        return enemies[Random. Range(0, enemies.Count)];;
    }

}
