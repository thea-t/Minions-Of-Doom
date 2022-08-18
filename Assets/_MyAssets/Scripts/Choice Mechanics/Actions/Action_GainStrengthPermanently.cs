using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Action choice that grants strength to the player
/// </summary>
public class Action_GainStrengthPermanently : Choice
{
    [FormerlySerializedAs("StrengthToGain")] [SerializeField] private int m_StrengthToGain;

    public override void OnExecute()
    {
        base.OnExecute();
        Player.StartingStrength += m_StrengthToGain;
    }
}