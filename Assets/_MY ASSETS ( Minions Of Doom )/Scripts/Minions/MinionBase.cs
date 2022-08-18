using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;


public enum MinionType
{
    None,
    Skillful,
    Elemental,
    Fighter
};

/// <summary>
/// Base card class that holds common minion functionalities.
/// </summary>
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


    /// <summary>
    /// Setting the card UI when the game starts/
    /// </summary>
    private void Start()
    {
        m_VisualMinion.SetCharacterLook();
        m_VisualMinion.VisualizeSpecialPower(true, m_MinionType, m_MinionData);
        m_VisualMinion.SetMinionCostUI(m_MinionData.cost);
        m_VisualMinion.SetMinionTitle(m_MinionData.name);
        m_VisualMinion.SetMinionDescription(m_MinionData.description);

        if (m_MinionData.minionRarity == MinionRarity.None)
        {
            m_MinionData.minionRarity = MinionRarity.Common;
        }

        m_NavAgent.enabled = true;
        m_StartScale = transform.localScale.x;
    }

    /// <summary>
    /// Setting the card data to a default one when the script is reset 
    /// </summary>
    protected virtual void Reset()
    {
        m_VisualMinion = GetComponent<VisualMinion>();
        m_RagdollToAnimator = GetComponent<RagdollToAnimator>();
        m_Animator = GetComponent<Animator>();
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_AudioSource = GetComponent<AudioSource>();
        grabbable = GetComponent<Grabbable>();
    }

    /// <summary>
    /// Hide visuals and show particles before the minion is rewarded.
    /// </summary>
    public void PrepareForDisplaying()
    {
        transform.localScale = Vector3.zero;
        m_NavAgent.enabled = false;
        m_VisualMinion.minionUiPopup.gameObject.SetActive(false);
        m_VisualMinion.VisualizeSpecialPower(false, m_MinionType, m_MinionData);
    }

    /// <summary>
    /// Change the scale of a minion based on a divider. The higher divider = the smaller minion
    /// </summary>
    public Vector3 SetMinionScale(float divider)
    {
        Vector3 startingScale = this.transform.localScale;
        return new Vector3(startingScale.x / divider, startingScale.y / divider, startingScale.z / divider);
    }

    /// <summary>
    /// Setting the snapzone that this minion is attached to.
    /// </summary>
    public void SetSnapZone(SnapZone snapZone)
    {
        m_SnapZone = snapZone;
    }

    /// <summary>
    /// Show the minion and reset it when drawn
    /// </summary>
    public void OnMinionDrawn()
    {
        Show();
        m_GroundTrigger.enabled = true;
        m_HeadCollider.enabled = true;
        m_NavAgent.enabled = false;
        m_VisualMinion.minionUiPopup.gameObject.SetActive(false);

        transform.position = m_SnapZone.transform.position;
        transform.rotation = Quaternion.Euler(0, m_SnapZone.transform.eulerAngles.y, 0);
    }

    /// <summary>
    /// This is called when the minion is grabbed by the player. Plays audio and checks if player has enough mana.
    /// </summary>
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

    /// <summary>
    /// This opens up a UI panel after the player grabs the minion, and displays the minions stats.
    /// </summary>
    void EnableUiPopUp()
    {
        m_VisualMinion.minionUiPopup.gameObject.SetActive(true);

        foreach (var minion in GameManager.Instance.DeckManager.handPile)
        {
            if (minion is FighterMinionBase)
            {

                string attackText = "Attack: ";
                minion.m_VisualMinion.SetSpecialPowerTMP(attackText, m_MinionData.damage,
                    GameManager.Instance.Player.Strength);
            }
        }
    }

    /// <summary>
    /// This is called after the minion collides with the play area.
    /// </summary>
    public void OnTableCollided()
    {
        Debug.Log("collided");
        m_RagdollToAnimator.ToggleRagdoll(false);
        m_GroundTrigger.enabled = false;
        m_VisualMinion.minionUiPopup.gameObject.SetActive(false);
        m_VisualMinion.VisualizeSpecialPower(false, m_MinionType, null);
        m_Animator.SetTrigger("Get Up");


        GameManager.Instance.DeckManager.handPile.Remove(this);
        GameManager.Instance.DeckManager.discardPile.Add(this);

        if (m_MinionData.isMale)
            m_AudioSource.clip = AudioManager.Instance.maleMinionOnTableCollided;
        else
            m_AudioSource.clip = AudioManager.Instance.femaleMinionOnTableCollided;

        m_AudioSource.Play();
        if (timerOnReleased != null)
        {
            StopCoroutine(timerOnReleased);
        }

        StartCoroutine(OnGetUp());
    }

    /// <summary>
    /// This starts a timer so that in case minion can't reach to its target.
    /// </summary>
    /// <returns></returns>
    IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(RESET_TIME);

        m_RagdollToAnimator.ToggleRagdoll(false);
        OnMinionDrawn();

        if (canBeGrabbed) GameManager.Instance.ManaManager.AddMana(m_MinionData.cost);
    }

    /// <summary>
    /// This is called when the player releases the minion.
    /// </summary>
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

    /// <summary>
    /// This is called after the minion stands up. Starts moving the minion towards the enemy.
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnGetUp()
    {
        yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);
        m_NavAgent.enabled = true;

        StartCoroutine(MoveToTarget());
    }

    /// <summary>
    /// Coroutine that moves the minion to the enemy and checks distance while moving to determine if arrived.
    /// </summary>
    /// <returns></returns>
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
        } while (m_NavAgent.remainingDistance > 0.2f || elapsedTime < 1.5f);

        StartCoroutine(UseMinionPower());
        m_Animator.SetBool("Run", false);
    }

    /// <summary>
    /// This is called when the minion arrives at the enemy. Plays the minions special ability.
    /// </summary>
    private IEnumerator UseMinionPower()
    {
        m_Animator.SetTrigger(m_MinionPowerAnimation);
        yield return new WaitForSeconds(2);
        GameManager.Instance.ManaManager.ManaToGainOnTurnBegin += m_MinionData.manaToGainOnTurnBegin;
        Player.CurrentHealth += m_MinionData.playerHealthToGain;
        
        GameManager.Instance.UiManager.UpdatePlayerHealth( Player.CurrentHealth, Player.MaxHealth);
        GameManager.Instance.UiManager.UpdateManaUI(GameManager.Instance.ManaManager.ManaToGainOnTurnBegin);
        Hide();
    }

    /// <summary>
    /// Rotates the minion to face the target.
    /// </summary>
    /// <param name="target"></param>
    protected void LookAtTarget(Transform target)
    {
        transform.DOLookAt(target.position, LOOK_SPEED);
    }

    /// <summary>
    /// Sets the minion visible with an animation
    /// </summary>
    private void Show()
    {
        transform.DOScale(m_StartScale, 0.5f);
    }

    /// <summary>
    /// Sets the minion invisible with an animation
    /// </summary>
    private void Hide()
    {
        transform.DOScale(0, 0.5f);
        m_VisualMinion.VisualizeSpecialPower(false, MinionType.None, null);
    }

    /// <summary>
    /// Animation event
    /// </summary>
    [UsedImplicitly]
    protected virtual void Attack()
    {

    }

    /// <summary>
    /// Animation event
    /// </summary>
    [UsedImplicitly]
    protected virtual void Defend()
    {

    }

    /// <summary>
    /// Animation event
    /// </summary>
    [UsedImplicitly]
    protected virtual void GainStrength()
    {

    }
}