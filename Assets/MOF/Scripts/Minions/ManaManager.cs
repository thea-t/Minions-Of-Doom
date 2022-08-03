using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private int ManaToGainOnTurnBegin;
    private int CurrentMana;
    private void Start()
    {
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
