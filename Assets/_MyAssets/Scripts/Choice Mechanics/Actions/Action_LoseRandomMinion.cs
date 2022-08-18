using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action choice that causes the player to lose a minion
/// </summary>
public class Action_LoseRandomMinion : Choice
{
    public override void OnExecute()
    {
        base.OnExecute();

        MinionBase minion =  Player.WonMinions[Random.Range(0, Player.WonMinions.Count)];
        Player.WonMinions.Remove(minion);
    }
}
