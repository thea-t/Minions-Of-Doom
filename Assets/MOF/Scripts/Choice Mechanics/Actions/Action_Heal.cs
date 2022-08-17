using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

public class Action_Heal : Choice {
    [SerializeField] private int HealAmount;
    public override void OnExecute()
    {
        base.OnExecute();
        Player.CurrentHealth += HealAmount;
    }
}
