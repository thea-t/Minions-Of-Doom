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
    None,
    Skillful,
    Elemental,
    Fighter
};

[RequireComponent(typeof(VisualMinion))]
public abstract class MinionBase : MonoBehaviour
{
    [SerializeField] protected MinionData m_MinionData;
    [SerializeField] protected VisualMinion m_VisualMinion;
    [SerializeField] private RagdollToAnimator m_RagdollToAnimator;
    [SerializeField] private Animator m_Animator;
    [SerializeField] public NavMeshAgent m_NavAgent;
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
    private float m_StartScale;
    protected string m_MinionPowerAnimation;
    
    public Grabbable grabbable { get; set; }
    private bool canBeGrabbed;


    //Setting the card UI when the game starts 
    private void Start()
    {
        m_VisualMinion.SetCharacterLook();
        m_VisualMinion.VisualizeSpecialPower(true, m_MinionType, m_MinionData);
        m_VisualMinion.SetMinionCostUI(m_MinionData.cost);
        m_VisualMinion.SetMinionTitle(m_MinionData.name);
        m_VisualMinion.SetMinionDescription(m_MinionData.description);
        
        if (m_MinionData.minionRarity == MinionRarity.None)  { m_MinionData.minionRarity = MinionRarity.Common; }
        
        m_NavAgent.enabled = true;
        m_StartScale = transform.localScale.x;
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

    public void PrepareForDisplaying()
    {
        transform.localScale = Vector3.zero;
        m_NavAgent.enabled = false;
        m_VisualMinion.minionUiPopup.gameObject.SetActive(false);
        m_VisualMinion.VisualizeSpecialPower(false, m_MinionType,m_MinionData);
    }
    
    //Change the scale of a minion based on a divider. The higher divider = the smaller minion
    public Vector3 SetMinionScale(float divider)
    {
        Vector3 startingScale = this.transform.localScale;
        return new Vector3(startingScale.x/divider, startingScale.y/divider, startingScale.z/divider);
    }
    public void SetSnapZone(SnapZone snapZone)
    {
        m_SnapZone = snapZone;
    }

    public void OnMinionDrawn() {
        Show();
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

            EnableUiPopUp();

            if (m_MinionData.isMale)
                m_AudioSource.clip = AudioManager.Instance.maleMinionOnGrabbed;
            else
                m_AudioSource.clip = AudioManager.Instance.femaleMinionOnGrabbed;

            m_AudioSource.Play();
        }
        else
        {
            if (m_MinionData.isMale)
                m_AudioSource.clip = AudioManager.Instance.maleMinionOnUnsuccessfulGrab;
            else
                m_AudioSource.clip = AudioManager.Instance.femaleMinionOnUnsuccessfulGrab;

            m_AudioSource.Play();

            m_Animator.SetTrigger("Shake Head");            
            m_VisualMinion.NotEnoughManaPopUp();

        }
    }

    void EnableUiPopUp() 
    {
        m_VisualMinion.minionUiPopup.gameObject.SetActive(true);

        foreach (var minion in GameManager.Instance.DeckManager.handPile) {
            if (minion is FighterMinionBase ) {
                
                string attackText = "Attack: ";
                minion.m_VisualMinion.SetSpecialPowerTMP(attackText, m_MinionData.damage, GameManager.Instance.Player.Strength);  
            }
        }
    }

    public void OnTableCollided()
    {
        Debug.Log("collided");
        m_RagdollToAnimator.ToggleRagdoll(false);
        m_GroundTrigger.enabled = false;
        m_VisualMinion.minionUiPopup.gameObject.SetActive(false);
        m_VisualMinion.VisualizeSpecialPower(false, m_MinionType, null);
        m_Animator.SetTrigger("Get Up");


        GameManager.Instance.DeckManager.handPile.Remove(this);
        if (!m_MinionData.exhausts)
        {
            GameManager.Instance.DeckManager.discardPile.Add(this);
        }

        if (m_MinionData.isMale)
            m_AudioSource.clip = AudioManager.Instance.maleMinionOnTableCollided;
        else
            m_AudioSource.clip = AudioManager.Instance.femaleMinionOnTableCollided;

        m_AudioSource.Play();
        if (timerOnReleased != null) {
            StopCoroutine(timerOnReleased);
        }
        
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
        if (m_MinionData.isMale)
        {
            m_AudioSource.clip = AudioManager.Instance.maleMinionOnDropped;
        }
        else
        {
            m_AudioSource.clip = AudioManager.Instance.femaleMinionOnDropped;
        }

        if (canBeGrabbed) m_AudioSource.Play();

        m_HeadCollider.enabled = false;
        timerOnReleased = StartCoroutine(ReturnToOriginalPosition());
    }
    
    private IEnumerator OnGetUp()
    { 
        yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);
        m_NavAgent.enabled = true;

        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        Transform enemyTransform = GameManager.Instance.EnemyManager.GetRandomEnemy().transform;
        m_Animator.SetBool("Run", true);
        m_NavAgent.destination = enemyTransform.position;
        LookAtTarget(enemyTransform);
        float elapsedTime = 0;
        
        //wait until the distance between target and minion is less than 0.2
        do
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        } 
        while (m_NavAgent.remainingDistance > 0.2f || elapsedTime < 1.5f);

        OnArrivalToTarget();
    }

    private void OnArrivalToTarget()
    {        
        StartCoroutine(UseMinionPower());       
        m_Animator.SetBool("Run", false);
    
    }
    
    private IEnumerator UseMinionPower()
    {               
        m_Animator.SetTrigger(m_MinionPowerAnimation);
        yield return new WaitForSeconds(2);
        
        Hide();
    }

    protected void LookAtTarget(Transform target)
    {
        transform.DOLookAt(target.position, LOOK_SPEED);
    }

    private void Show() {
        transform.DOScale(m_StartScale,0.5f);
    }

    private void Hide() {
        transform.DOScale(0,0.5f);
        m_VisualMinion.VisualizeSpecialPower(false, MinionType.None, null);
    }
    
    //anim event
    protected virtual void Attack() 
    {
        
    } 
    //anim event
    protected virtual void Defend() 
    {
        
    } 
    //anim event
    protected virtual void GainStrength() 
    {
        
    } 
}