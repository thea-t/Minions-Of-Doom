using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private int m_StartingMana;
    [SerializeField] private int m_MaxMana;
    public int CurrentMana { get; set; }

    private void Start()
    {
        CurrentMana = m_StartingMana;
    }
    
    public bool TryToGrabMinion(int cost)
    {
        Debug.Log("TRY TO GRAB");
        if (cost <= CurrentMana)
        {
            CurrentMana -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
