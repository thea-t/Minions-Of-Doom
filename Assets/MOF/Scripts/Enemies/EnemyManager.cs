using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public List <EnemyBase> enemies;

    private void Start()
    {
        Debug.Log("enemies count "+enemies.Count);
        
        GameManager.Instance.TurnManager.EnemyTurn += OnEnemyTurn;

        foreach (var enemy in enemies)
        {
            enemy.Dead += CheckRemainingEnemyCount;
        }
    }

    private void CheckRemainingEnemyCount()
    {
        if (enemies.Count == 0)
        {
            GameManager.Instance.RewardManager.SpawnRewardPrefab();
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

    public EnemyBase GetSelectedEnemy()
    {
        EnemyBase rand = enemies[Random. Range(0, enemies.Count)];
        return rand;
    }

}
