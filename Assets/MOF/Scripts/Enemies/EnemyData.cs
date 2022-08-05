using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Data used by the Enemies (see EnemyBase.cs)
// The reason why this is scriptable object rather than ordinary class is mainly because same data can be used by different enemies 
[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int[] attackDamage; // used to assign different attack damage to the enemy every turn 
    public int maxHealth;
    public EnemyType enemyType;

}
public enum EnemyType
{
    None,
    Monster,
    Beast,
    Boss
};
