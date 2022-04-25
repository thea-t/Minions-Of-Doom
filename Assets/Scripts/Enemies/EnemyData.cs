using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int[] attackDamage;
    public int health;
    public int maxHealth;
}
