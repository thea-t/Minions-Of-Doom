using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    private Vector3 dragBeginPos;
    private Vector3 dragBeginRot;
    private Vector3 screenPoint;
    private Vector3 offset;
    
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
    
    //https://forum.unity.com/threads/drag-drop-game-objects-without-rigidbody-with-the-mouse.64169/
    
    public void OnDragBegin()
    {
        dragBeginPos = transform.position;
        dragBeginRot = transform.rotation.eulerAngles;
        
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        transform.DORotate(Vector3.zero, 0.3f);
    }
    public void OnDrag()
    { 
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z-0.1f);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
    }
    public void OnDragEnd()
    {
        ReturnToHand();
    }

    private void ReturnToHand()
    {
        transform.DOMove(dragBeginPos, 0.3f);
        transform.DORotate(dragBeginRot, 0.3f);
    }

}
