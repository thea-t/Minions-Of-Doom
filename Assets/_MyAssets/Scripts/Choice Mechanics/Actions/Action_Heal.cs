using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Action choice that heals the player
/// </summary>
public class Action_Heal : Choice {
    
    [FormerlySerializedAs("HealAmount")] [SerializeField] private int m_HealAmount;
    
    public override void OnExecute()
    {
        base.OnExecute();
        Player.CurrentHealth += m_HealAmount;
    }
}
