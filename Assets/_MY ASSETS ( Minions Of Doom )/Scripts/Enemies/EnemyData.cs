using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data used by the Enemies (see EnemyBase.cs)
/// The reason why this is scriptable object rather than ordinary class is mainly because same data can be used by different enemies 
/// </summary>
[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int[] attackDamage; 
    public int maxHealth;
    public EnemyType enemyType;

}
public enum EnemyType
{
    None = 0,
    Monster = 1,
    Beast = 2,
    Boss = 3
};
