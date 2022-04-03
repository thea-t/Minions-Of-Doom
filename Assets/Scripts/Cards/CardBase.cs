using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base card class that holds common card functionalities.

public enum CardType { Skill, Element, Minion };

[RequireComponent(typeof(VisualCard))]
public abstract class CardBase : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int m_Cost;
    [SerializeField] private string m_Title;
    [SerializeField] private string m_Description;
    
    [SerializeField] protected CardType m_CardType;
    

    private VisualCard m_VisualCard;
    private void Start()
    {
        m_VisualCard = GetComponent<VisualCard>();
        
        m_VisualCard.SetCardTypeUI(m_CardType);
        m_VisualCard.SetCardCostUI(m_Cost);
        m_VisualCard.SetCardTitle(m_Title);
        m_VisualCard.SetCardDescription(m_Description);
    }

    protected virtual void Reset()
    {
        m_Cost = 0;
        m_Title = "Title";
        m_Description = "Write something about this card here.";
    }

    public virtual void OnCardDrawn()
    {
    }

}
