using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fighter minions focus on dealing damage to enemies
/// </summary>
public class FighterMinionBase : MinionBase
{ 
    /// <summary>
    /// Setting the protected animation which is played by the base class.
    /// </summary>
    void Awake() 
    {
        m_MinionType = MinionType.Fighter;
        m_MinionPowerAnimation = "Hit";
    }
    
    /// <summary>
    /// Resetting automatically sets minion type to fighter by default.
    /// </summary>
    protected override void Reset()
    {
        base.Reset();
        m_MinionType = MinionType.Fighter;
    }
    
    
    /// <summary>
    /// Animation event. Attacks the enemy.
    /// </summary>
    protected override void Attack() 
    {
        base.Attack();
        int damage = m_MinionData.damage + GameManager.Instance.Player.Strength;
        GameManager.Instance.EnemyManager.GetRandomEnemy().TakeDamage(damage);
    }
}

