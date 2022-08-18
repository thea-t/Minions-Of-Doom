using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Action choice which increases the mana total of the player permanently.
/// </summary>
public class Action_StartTurnWithExtraMana : Choice
{
    [FormerlySerializedAs("ExtraManaAmount")] [SerializeField] private int m_ExtraManaAmount;

    public override void OnExecute()
    {
        base.OnExecute();
        Player.StartingMana += m_ExtraManaAmount;
    }
}