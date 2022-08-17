using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GainStrengthPermanently :  Choice {
    [SerializeField] private int StrengthToGain;
    public override void OnExecute()
    {
        base.OnExecute();
        Player.StartingStrength += StrengthToGain;
    }
}
