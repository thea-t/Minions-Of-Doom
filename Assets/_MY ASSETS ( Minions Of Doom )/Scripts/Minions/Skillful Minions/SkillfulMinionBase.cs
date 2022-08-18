using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skillful minions focus on shielding the player to block damage.
/// </summary>
public class SkillfulMinionBase : MinionBase
{
    /// <summary>
    /// Setting the protected animation which is played by the base class.
    /// </summary>
    void Awake() 
    {
        m_MinionType = MinionType.Skillful;
        m_MinionPowerAnimation = "Block";
    }
    /// <summary>
    /// Resetting automatically sets minion type to skillful by default.
    /// </summary>
    protected override void Reset()
    {
        base.Reset();
        m_MinionType = MinionType.Skillful;
    }
    
    /// <summary>
    /// Animation event. Gains shield.
    /// </summary>
    protected override void Defend() 
    {
        base.Defend();
        GameManager.Instance.Player.Block += m_MinionData.block;        
        GameManager.Instance.UiManager.UpdateBlockUI(GameManager.Instance.Player.Block);

    }
}
