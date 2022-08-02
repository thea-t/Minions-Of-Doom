using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ElementalMinionBase : MinionBase {
    //Setting the cart type when the script is reset 

    void Start() 
    {
        m_MinionType = MinionType.Elemental;
        m_MinionPowerAnimation = "";
    }
    
    protected override void Reset()
    {
        base.Reset();
        m_MinionType = MinionType.Elemental;
    }
    //anim event
    protected override void GainStrength() 
    {
        base.GainStrength();
        GameManager.Instance.Player.CurrentStrength += m_MinionData.strength;
        GameManager.Instance.UiManager.UpdateStrengthUI(GameManager.Instance.Player.CurrentStrength);
    }

}
