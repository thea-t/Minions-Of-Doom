using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Scriptable object that holds reward data used to design which minions can spawn from a reward.
/// </summary>
[CreateAssetMenu]
public class RewardData : ScriptableObject
{
    [SerializeField] MinionBase[] m_CommonMinions;
    [SerializeField] MinionBase[] m_RareMinions;
    [SerializeField] MinionBase[] m_LegendaryMinions;

    /// <summary>
    /// Returns a minion based on the enemy type. Monster enemies return a common minion, beasts return a rare minion, bosses return a legendary minion as a reward
    /// </summary>
    public MinionBase MinionReward(EnemyType enemyType)
    {
        MinionBase commonMinion = m_CommonMinions[Random.Range(0, m_CommonMinions.Length)];
        MinionBase rareMinion = m_RareMinions[Random.Range(0, m_RareMinions.Length)];
        MinionBase legendaryMinion = m_LegendaryMinions[Random.Range(0, m_LegendaryMinions.Length)];
        
        switch (enemyType)
        {
            case EnemyType.Monster: return commonMinion;
            
            case  EnemyType.Beast: return rareMinion; 
            
            case EnemyType.Boss: return legendaryMinion;
            
            default: return commonMinion;
        }
    }

}
