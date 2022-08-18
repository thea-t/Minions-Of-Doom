using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Action choice that gives extra block to the player.
/// </summary>
public class Action_GainBlockPermanently : Choice
{

    [FormerlySerializedAs("BlockToGain")] [SerializeField] private int m_BlockToGain;

    public override void OnExecute()
    {
        base.OnExecute();
        Player.StartingShield += m_BlockToGain;
    }
}