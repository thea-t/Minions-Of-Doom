using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

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
    private const float LOOK_SPEED = 1;
    

    [SerializeField] private Collider m_GroundTrigger;
    [SerializeField] private Collider m_HeadCollider;

    protected MinionType m_MinionType;
    private EnemyBase m_TargetedEnemy;
    private EnemyBase m_PreviouslyTargetedEnemy;
    private SnapZone m_SnapZone;

    private Vector3 m_DragBeginPos;
    private Vector3 m_DragBeginRot;
    private Vector3 m_ScreenPoint;
    private Vector3 m_Offset;


    public Grabbable grabbable { get; set; }
    private bool canBeGrabbed;


    //Setting the card UI when the game starts 
    private void Start()
    {
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

    public void SetSnapZone(SnapZone snapZone)
    {
        m_SnapZone = snapZone;
    }

    public void OnMinionDrawn()
    {
        m_GroundTrigger.enabled = true;
        m_HeadCollider.enabled = true;
        m_NavAgent.enabled = false;
        m_VisualMinion.minionUiPopup.gameObject.SetActive(false);

        transform.position = m_SnapZone.transform.position;
        transform.rotation = Quaternion.Euler(0, m_SnapZone.transform.eulerAngles.y, 0);
    }

    public void OnGrab()
    {
        canBeGrabbed = GameManager.Instance.ManaManager.TryToGrabMinion(m_MinionData.cost);

        if (canBeGrabbed)
        {
            m_RagdollToAnimator.ToggleRagdoll(true);

            m_VisualMinion.minionUiPopup.gameObject.SetActive(true);

            if (m_MinionData.isMale)
                m_AudioSource.clip = GameManager.Instance.AudioManager.maleMinionOnGrabbed;
            else
                m_AudioSource.clip = GameManager.Instance.AudioManager.femaleMinionOnGrabbed;

            m_AudioSource.Play();
        }
        else
        {
            if (m_MinionData.isMale)
                m_AudioSource.clip = GameManager.Instance.AudioManager.maleMinionOnUnsuccessfulGrab;
            else
                m_AudioSource.clip = GameManager.Instance.AudioManager.femaleMinionOnUnsuccessfulGrab;

            m_AudioSource.Play();

            m_Animator.SetTrigger("Shake Head");
        }
    }

    public void OnTableCollided()
    {
        Debug.Log("collided");
        m_RagdollToAnimator.ToggleRagdoll(false);
        m_GroundTrigger.enabled = false;
        m_VisualMinion.minionUiPopup.gameObject.SetActive(false);
        m_VisualMinion.SetMinionParticle(m_MinionType, false);
        m_Animator.SetTrigger("Get Up");


        GameManager.Instance.DeckManager.handPile.Remove(this);
        if (!m_MinionData.exhausts)
        {
            GameManager.Instance.DeckManager.discardPile.Add(this);
        }

        if (m_MinionData.isMale)
            m_AudioSource.clip = GameManager.Instance.AudioManager.maleMinionOnTableCollided;
        else
            m_AudioSource.clip = GameManager.Instance.AudioManager.femaleMinionOnTableCollided;

        m_AudioSource.Play();
        StopCoroutine(timerOnReleased);
        StartCoroutine(OnGetUp());
    }

    IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(RESET_TIME);

        m_RagdollToAnimator.ToggleRagdoll(false);
        OnMinionDrawn();

        if (canBeGrabbed) GameManager.Instance.ManaManager.AddMana(m_MinionData.cost);
    }

    public void OnReleased()
    {
        Debug.Log("released");
        if (m_MinionData.isMale)
        {
            m_AudioSource.clip = GameManager.Instance.AudioManager.maleMinionOnDropped;
        }
        else
        {
            m_AudioSource.clip = GameManager.Instance.AudioManager.femaleMinionOnDropped;
        }

        if (canBeGrabbed) m_AudioSource.Play();

        m_HeadCollider.enabled = false;
        timerOnReleased = StartCoroutine(ReturnToOriginalPosition());
    }
    
    private IEnumerator OnGetUp()
    { 
        yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);
        m_NavAgent.enabled = true;

        OnArriveToEnemy();
        //StartCoroutine(MoveToTarget());
    }

    private void OnArriveToEnemy()
    {
        Debug.Log("1");
        Transform enemyTransform = GameManager.Instance.EnemyManager.GetSelectedEnemy().transform;
        
        m_NavAgent.destination = enemyTransform.position;
        LookAtTarget(enemyTransform);
        
        if (!m_NavAgent.pathPending)
        {
            if (m_NavAgent.remainingDistance <= m_NavAgent.stoppingDistance)
            {
                if (!m_NavAgent.hasPath || m_NavAgent.velocity.sqrMagnitude == 0f)
                {
                    Attack();
                    Debug.Log("2");
                }
            }
        }
    }

    private IEnumerator MoveToTarget()
    {                    
        Debug.Log("1");

        Transform enemyTransform = GameManager.Instance.EnemyManager.GetSelectedEnemy().transform;
        
        m_NavAgent.destination = enemyTransform.position;
        LookAtTarget(enemyTransform);

        //wait until the distance between target and minion is less than 0.2
        do
        {
            yield return null;
            Debug.Log("2");

        } while (m_NavAgent.remainingDistance > 0.2f);
        Debug.Log("3");


    }
    
    private void Attack()
    {
        m_Animator.SetTrigger("Left ");
    }
    
    protected void LookAtTarget(Transform target)
    {
        transform.DOLookAt(target.position, LOOK_SPEED);
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