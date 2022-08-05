using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu]
public class RewardData : ScriptableObject
{
    [SerializeField] MinionBase[] m_CommonMinions;
    [SerializeField] MinionBase[] m_RareMinions;
    [SerializeField] MinionBase[] m_LegendaryMinions;

    public MinionBase MinionReward(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Monster: return m_CommonMinions[Random.Range(0, m_CommonMinions.Length)];
            
            case  EnemyType.Beast: return m_RareMinions[Random.Range(0, m_RareMinions.Length)]; 
            
            case EnemyType.Boss: return m_LegendaryMinions[Random.Range(0, m_LegendaryMinions.Length)];
            
            default: return m_CommonMinions[Random.Range(0, m_CommonMinions.Length)];
        }
    }

}
