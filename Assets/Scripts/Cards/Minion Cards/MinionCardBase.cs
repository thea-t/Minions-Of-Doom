using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCardBase : CardBase
{
    [SerializeField] private int m_Attack;
    
    protected override void Reset()
    {
        base.Reset();
        m_CardType = CardType.Minion;
    }

    protected override void AttackTargetedEnemy(EnemyBase target)
    {
        base.AttackTargetedEnemy(target);
        
    }
    
}
