using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamagable
{
    [SerializeField] private VisualEnemy m_VisualEnemy;
    [SerializeField] private Animator m_Animator;
    public EnemyData EnemyData;

    protected string m_AttackAnimation;

    public event Action Dead;
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int Block { get; set; }

    /// <summary>
    /// Getting and assigning components on Reset in order to save time from dragging and dropping them
    /// </summary>
    void Reset()
    {
        m_Animator = GetComponent<Animator>();
        m_VisualEnemy = GetComponent<VisualEnemy>();
    }

    /// <summary>
    /// Setting the enemy's health and updating its UI.
    /// Resetting the turn count as this int is used for setting different attack values each turn
    /// Subscribing to TurnManager's event and listening for the enemy's turn in order to attack the player
    /// </summary>
    void Start()
    {
        m_VisualEnemy.SetCharacterLook();
        MaxHealth = EnemyData.maxHealth;
        CurrentHealth = EnemyData.maxHealth;

        m_VisualEnemy.UpdateHealthUI(CurrentHealth);
        m_VisualEnemy.UpdateAttackUI(EnemyData.attackDamage[GameManager.Instance.TurnManager.TurnCount]);

        //notify  the enemy manager that this enemy has just spawned
        GameManager.Instance.EnemyManager.OnEnemySpawned(this);
    }

    /// <summary>
    /// Reducing enemy's health and updating its UI when the enemy takes damage
    /// </summary>
    public void TakeDamage(int amount)
    {

        CurrentHealth -= amount;
        m_Animator.SetTrigger("Take Damage");

        m_VisualEnemy.UpdateHealthUI(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Starts the dying animation
    /// </summary>
    public void Die()
    {
        StartCoroutine(StartDying());
    }

    /// <summary>
    /// Dying animation.
    /// </summary>
    private IEnumerator StartDying()
    {
        m_Animator.SetTrigger("Die");
        GameManager.Instance.EnemyManager.enemies.Remove(this);
        Dead?.Invoke();

        yield return new WaitForSeconds(DeathAnimationDuration() / 1.5f);
        transform.DOScale(Vector3.zero, DeathAnimationDuration() / 2f).onComplete =
            () => { this.gameObject.SetActive(false); };
        //play particle and sound
    }

    /// <summary>
    /// Checks all animations to find the death animation clip and returns its duration.
    /// </summary>
    /// <returns></returns>
    private float DeathAnimationDuration()
    {
        RuntimeAnimatorController ac = m_Animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "Die")
            {
                return ac.animationClips[i].length;
            }
        }

        return 0.1f;
    }

    /// <summary>
    /// This is called when the enemy's turn begins. Triggers attack animation and sets ui.
    /// </summary>
    public void OnEnemyTurn()
    {
        StartCoroutine(DealingDamageToPlayer());

        Debug.Log("turnCount: " + GameManager.Instance.TurnManager.TurnCount);
    }

    /// <summary>
    /// Playing animation while the enemy is attacking and dealing damage to the player at the end of the animation
    /// Also notifying the Turn Manager that the turn is over
    /// 
    /// How to get animation duration: https://answers.unity.com/questions/692593/get-animation-clip-length-using-animator.html
    /// </summary>
    private IEnumerator DealingDamageToPlayer() 
    {
        yield return null;
        
        m_Animator.SetTrigger("Hit");

        yield return new WaitForSeconds(2);

        int turnCount = GameManager.Instance.TurnManager.TurnCount;
        if (turnCount < EnemyData.attackDamage.Length)
        {
            GameManager.Instance.Player.TakeDamage(EnemyData.attackDamage[turnCount]);
        }
        else
        {
            GameManager.Instance.TurnManager.TurnCount = 0;
        }

        GameManager.Instance.TurnManager.EndEnemyTurn();
        m_VisualEnemy.UpdateAttackUI(EnemyData.attackDamage[GameManager.Instance.TurnManager.TurnCount]);
    }
}