using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GainBlockPermanently : Choice {
    [SerializeField] private int BlockToGain;
    public override void OnExecute()
    {
        base.OnExecute();
        Player.StartingShield += BlockToGain;
    }
}
