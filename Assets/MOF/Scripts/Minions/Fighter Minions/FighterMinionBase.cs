using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMinionBase : MinionBase
{ 
    void Awake() 
    {
        m_MinionType = MinionType.Fighter;
        m_MinionPowerAnimation = "Hit";
    }
    
    //Setting the cart type when the script is reset 
    protected override void Reset()
    {
        base.Reset();
        m_MinionType = MinionType.Fighter;
    }
    
    //anim event
    protected override void Attack() 
    {
        base.Attack();
        int damage = m_MinionData.damage + GameManager.Instance.Player.Strength;
        GameManager.Instance.EnemyManager.GetSelectedEnemy().TakeDamage(damage);
    }
}

