using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_StartTurnWithLessMana :  Choice {
    [SerializeField] private int LessManaAmount;
    public override void OnExecute()
    {
        base.OnExecute();
        Player.StartingMana -= LessManaAmount;
    }
}
