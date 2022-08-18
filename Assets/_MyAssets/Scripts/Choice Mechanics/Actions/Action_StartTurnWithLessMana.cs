using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Action choice which decreases the mana total of the player permanently.
/// </summary>
public class Action_StartTurnWithLessMana : Choice
{
    [FormerlySerializedAs("LessManaAmount")] [SerializeField] private int m_LessManaAmount;

    public override void OnExecute()
    {
        base.OnExecute();
        Player.StartingMana -= m_LessManaAmount;
    }
}