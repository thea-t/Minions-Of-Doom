using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_StartTurnWithExtraMana :  Choice {
    [SerializeField] private int ExtraManaAmount;
    public override void OnExecute()
    {
        base.OnExecute();
        Player.StartingMana += ExtraManaAmount;
    }
}
