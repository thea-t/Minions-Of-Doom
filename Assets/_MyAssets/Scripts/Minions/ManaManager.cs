using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public int ManaToGainOnTurnBegin { get; set; }
    private int CurrentMana { get; set; }

    private void Start()
    {
        ManaToGainOnTurnBegin = Player.StartingMana;
        CurrentMana = ManaToGainOnTurnBegin;
        GameManager.Instance.UiManager.UpdateManaUI(CurrentMana);
    }

    /// <summary>
    /// Returns true if the player has enough mana and can grab the minion.
    /// </summary>
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

    /// <summary>
    /// Increases the players mana by amount.
    /// </summary>
    public void AddMana(int amount)
    {
        CurrentMana += amount;
        GameManager.Instance.UiManager.UpdateManaUI(CurrentMana);
    }

    /// <summary>
    /// This is called when the players turn start. It resets mana back to full.
    /// </summary>
    public void RechargeManaOnTurnBegin()
    {
        CurrentMana = ManaToGainOnTurnBegin;
        GameManager.Instance.UiManager.UpdateManaUI(CurrentMana);
    }

}
