using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

//Base card class that holds common card functionalities.

public enum MinionType
{
    Skillful,
    Elemental,
    Fighter
};
[RequireComponent(typeof(VisualMinion))]
public abstract class MinionBase : MonoBehaviour
{
    [SerializeField] private MinionData m_MinionData;
    [SerializeField] private VisualMinion m_VisualMinion;
    
    protected MinionType m_MinionType;
    private EnemyBase m_TargetedEnemy;
    private EnemyBase m_PreviouslyTargetedEnemy;


    private Vector3 dragBeginPos;
    private Vector3 dragBeginRot;
    private Vector3 screenPoint;
    private Vector3 offset;

    public Grabbable grabbable{ get; set; }
    
    

    //Setting the card UI when the game starts 
    private void Start() {
        m_VisualMinion.SetCharacterLook(m_MinionData);
        m_VisualMinion.SetMinionTypeParticle(m_MinionType);
        m_VisualMinion.SetMinionCostUI(m_MinionData.cost);
        m_VisualMinion.SetMinionTitle(m_MinionData.name);
        m_VisualMinion.SetMinionDescription(m_MinionData.description);
    }

    //Setting the card data to a default one when the script is reset 
    protected virtual void Reset()
    {
        m_VisualMinion = GetComponent<VisualMinion>();
        grabbable = GetComponent<Grabbable>();
    }

    public virtual void OnMinionDrawn()
    {
    }

    #region Deprecated
    /*
    
    
    public void OnDragBegin()
    {
        dragBeginPos = transform.position;
        dragBeginRot = transform.rotation.eulerAngles;

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position -
                 Camera.main.ScreenToWorldPoint(
                     new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        transform.DORotate(Vector3.zero, 0.3f);

        this.GetComponent<Collider>().enabled = false;
    }

    public void OnDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z - 0.1f);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;

        TargetEnemy();
    }

    public void OnDragEnd()
    {
        if (m_TargetedEnemy)
        {
            m_TargetedEnemy.TakeDamage(m_Attack);
           // AttackTargetedEnemy(m_TargetedEnemy);
        }
        else
        {
            ReturnToHand();
        this.GetComponent<Collider>().enabled = true;
        }
    }

    private void ReturnToHand()
    {
        transform.DOMove(dragBeginPos, 0.3f);
        transform.DORotate(dragBeginRot, 0.3f);
    } 
    private void TargetEnemy()
    {
        
       // m_TargetedEnemy = GameManager.Instance.RaycastManager.GetByRay<EnemyBase>();

        if (m_TargetedEnemy)
        {
            m_PreviouslyTargetedEnemy = m_TargetedEnemy;
            m_TargetedEnemy.HoveringWithCard(true);
            transform.DOMove(new Vector3(m_TargetedEnemy.visualEnemy.cardSnapPoint.transform.position.x, m_TargetedEnemy.visualEnemy.cardSnapPoint.transform.position.y, transform.position.z) , 0.5f);
        }
        
        if (m_PreviouslyTargetedEnemy && m_TargetedEnemy == null)
        {
            m_PreviouslyTargetedEnemy.HoveringWithCard(false);
        }
    }

*/
    #endregion

    protected virtual void AttackTargetedEnemy(EnemyBase target)
    {
        
    }

}