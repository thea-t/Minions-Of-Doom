using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_LoseRandomMinion : Choice
{
    public override void OnExecute()
    {
        base.OnExecute();

        MinionBase minion =  Player.WonMinions[Random.Range(0, Player.WonMinions.Count)];
        Player.WonMinions.Remove(minion);
    }
}
