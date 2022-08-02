using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMinionBase : MinionBase
{ 
    void Start() 
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
        GameManager.Instance.EnemyManager.GetSelectedEnemy().TakeDamage(m_MinionData.damage);
    }
}

