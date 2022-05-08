using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int[] attackDamage;
    public int maxHealth;

    [SerializeField] private EnemyBase enemyPrefab;

    void Spawner()
    {
        enemyPrefab.enemyData = this;
    }
    
}
