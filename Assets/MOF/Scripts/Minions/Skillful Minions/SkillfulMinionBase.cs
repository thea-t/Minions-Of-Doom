using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillfulMinionBase : MinionBase
{  
    void Awake() 
    {
        m_MinionType = MinionType.Skillful;
        m_MinionPowerAnimation = "Block";
    }
    //Setting the cart type when the script is reset 
    protected override void Reset()
    {
        base.Reset();
        m_MinionType = MinionType.Skillful;
    }
    
//anim event
    protected override void Defend() 
    {
        base.Defend();
        GameManager.Instance.Player.Block += m_MinionData.block;        
        GameManager.Instance.UiManager.UpdateBlockUI(GameManager.Instance.Player.Block);

    }
}
