using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardBase : CardBase
{
    protected override void Reset()
    {
        base.Reset();
        m_CardType = CardType.Skill;
    }

}
