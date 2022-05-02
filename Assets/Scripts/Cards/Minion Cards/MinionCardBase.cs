using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCardBase : CardBase
{ 
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
