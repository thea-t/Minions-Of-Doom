using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private RagdollToAnimator m_RagdollToAnimator;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private NavMeshAgent m_NavAgent;
    [SerializeField] private AudioSource m_AudioSource;
    
    Coroutine timerOnReleased;
    private const float RESET_TIME = 1;
    
    [SerializeField] private Collider m_GroundTrigger;
    [SerializeField] private Collider m_HeadCollider;
    
    protected MinionType m_MinionType;
    private EnemyBase m_TargetedEnemy;
    private EnemyBase m_PreviouslyTargetedEnemy;


    private Vector3 dragBeginPos;
    private Vector3 dragBeginRot;
    private Vector3 screenPoint;
    private Vector3 offset;

    public Grabbable grabbable{ get; set; }
    private bool recentGrabFailed = false;

    //Setting the card UI when the game starts 
    private void Start() {
        m_VisualMinion.SetCharacterLook();
        m_VisualMinion.SetMinionParticle(m_MinionType, true);
        m_VisualMinion.SetMinionCostUI(m_MinionData.cost);
        m_VisualMinion.SetMinionTitle(m_MinionData.name);
        m_VisualMinion.SetMinionDescription(m_MinionData.description);
    }

    //Setting the card data to a default one when the script is reset 
    protected virtual void Reset()
    {
        m_VisualMinion = GetComponent<VisualMinion>();
        m_RagdollToAnimator = GetComponent<RagdollToAnimator>();
        m_Animator = GetComponent<Animator>();
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_AudioSource = GetComponent<AudioSource>();
        grabbable = GetComponent<Grabbable>();
    }

    public virtual void OnMinionDrawn()
    { 
        m_GroundTrigger.enabled = true;
        m_HeadCollider.enabled = true;

        m_NavAgent.enabled = false;
    }
    
    public void OnGrab()
    {
       /* bool canBeGrabbed = GameManager.Instance.deckManager.TryToGrabMinion(minionData.minionCost);

        if (canBeGrabbed)
        {*/
            m_RagdollToAnimator.ToggleRagdoll(true);

            m_VisualMinion.minionUiPopup.gameObject.SetActive(true);

            if (m_MinionData.isMale)
                m_AudioSource.clip = GameManager.Instance.AudioManager.maleMinionOnGrabbed;
            else
                m_AudioSource.clip = GameManager.Instance.AudioManager.femaleMinionOnGrabbed;
                m_AudioSource.Play();
       /* }
        else
        {
            if(m_MinionData.isMale)
                m_AudioSource.clip = GameManager.Instance.AudioManager.maleMinionOnUnsuccessfulGrab;
            else
                m_AudioSource.clip = GameManager.Instance.AudioManager.femaleMinionOnUnsuccessfulGrab;
            m_AudioSource.Play();

            animator.SetTrigger("Shake Head");

            recentGrabFailed = true;
        }*/
         
         
    }
    public void OnTableCollided()
    {
       /* if (GameManager.Instance.Player.remainingMana >= minionData.minionCost)
        {*/
       Debug.Log("collided");
            m_RagdollToAnimator.ToggleRagdoll(false);
            m_GroundTrigger.enabled = false;
            m_VisualMinion.minionUiPopup.gameObject.SetActive(false);
            m_VisualMinion.SetMinionParticle(m_MinionType, false);
            m_Animator.SetTrigger("Get Up");

          //  minionHome.HideCostText();

            GameManager.Instance.DeckManager.handPile.Remove(this);
            if (!m_MinionData.exhausts)
            {
                GameManager.Instance.DeckManager.discardPile.Add(this);
              //  GameManager.Instance.DeckManager.recentlyPlayedMinions.Add(this);

            }

           /* GameManager.Instance.player.remainingMana -= minionData.minionCost;
            GameManager.Instance.uiManager.UpdateManaText();*/


            if (m_MinionData.isMale)
                m_AudioSource.clip = GameManager.Instance.AudioManager.maleMinionOnTableCollided;
            else
                m_AudioSource.clip = GameManager.Instance.AudioManager.femaleMinionOnTableCollided;
            m_AudioSource.Play();

            StopCoroutine(timerOnReleased);
        /*}
        else
        {
            Debug.LogError("Not enough mana. I don't think this is supposed to happen as this function isn't supposed to be called unless the mana is enough.");
        }*/
    }
    
   IEnumerator ReturToOriginalPosition()
    {
        yield return new WaitForSeconds(RESET_TIME);

       // GameManager.Instance.DeckManager.grabbedManaTotal += minionData.minionCost;

        m_RagdollToAnimator.ToggleRagdoll(false);
        OnMinionDrawn();
    }
    
    
    public void OnReleased()
    {
        if (!recentGrabFailed)
        {
            if (m_MinionData.isMale)
                m_AudioSource.clip = GameManager.Instance.AudioManager.maleMinionOnDropped;
            else
                m_AudioSource.clip = GameManager.Instance.AudioManager.femaleMinionOnDropped;
            m_AudioSource.Play();

          //  HideDescriptionUI();

            timerOnReleased = StartCoroutine(ReturToOriginalPosition());

            m_HeadCollider.enabled = false;
        }
        else
        {
            recentGrabFailed = false;
        }
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
    

  
}