using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public int ManaToGainOnTurnBegin { get; set; }
    private int CurrentMana;
    private void Start() {
        ManaToGainOnTurnBegin = Player.StartingMana;
        CurrentMana = ManaToGainOnTurnBegin;
        GameManager.Instance.UiManager.UpdateManaUI(CurrentMana);
    }
    
    public bool TryToGrabMinion(int cost)
    {
        if (cost <= CurrentMana)
        {
            CurrentMana -= cost;
            GameManager.Instance.UiManager.UpdateManaUI(CurrentMana);
            return true;
        }
            return false;
    }

    public void AddMana(int amount)
    {
        CurrentMana += amount;
        GameManager.Instance.UiManager.UpdateManaUI(CurrentMana);
    }

    public void RechargeManaOnTurnBegin()
    {
        CurrentMana = ManaToGainOnTurnBegin;
        GameManager.Instance.UiManager.UpdateManaUI(CurrentMana);
    }

}
