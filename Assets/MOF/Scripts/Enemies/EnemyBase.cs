using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBase :  MonoBehaviour, IDamagable
{
    [SerializeField] private VisualEnemy m_VisualEnemy;
    [SerializeField] private EnemyData m_EnemyData;
    [SerializeField] private Animator m_Animator;

    protected string m_AttackAnimation;

    public Action Dead;
    public int MaxHealth { get; set; }
 public int CurrentHealth { get; set; }
 public int Block { get; set; }

 //Getting and assigning components on Reset in order to save time from dragging and dropping them
 void Reset()
 {
     m_Animator = GetComponent<Animator>();
     m_VisualEnemy = GetComponent<VisualEnemy>();
 }
 
 //Setting the enemy's health and updating its UI.
 //Resetting the turn count as this int is used for setting different attack values each turn
 //Subscribing to TurnManager's event and listening for the enemy's turn in order to attack the player
 
 void Start()
 {
     m_VisualEnemy.SetCharacterLook();
     MaxHealth = m_EnemyData.maxHealth;
     CurrentHealth = m_EnemyData.maxHealth;
     
     m_VisualEnemy.UpdateHealthUI(CurrentHealth);
     m_VisualEnemy.UpdateAttackUI(m_EnemyData.attackDamage[GameManager.Instance.TurnManager.turnCount]);
     GameManager.Instance.EnemyManager.enemies.Add(this);
 }

 //Reducing enemy's health and updating its UI when the enemy takes damage
 public void TakeDamage(int amount)
 {
     
     CurrentHealth -= amount;
         m_Animator.SetTrigger("Take Damage");
     
     m_VisualEnemy.UpdateHealthUI(CurrentHealth);
     
     if (CurrentHealth <= 0) {
         Die();
     }
     

 }

public void Die() 
{
    StartCoroutine(StartDying());
}

private IEnumerator StartDying()
{
    m_Animator.SetTrigger("Die");
    GameManager.Instance.EnemyManager.enemies.Remove(this);
    Dead?.Invoke();
    
    yield return new WaitForSeconds(Time()/1.5f);
    transform.DOScale(Vector3.zero, Time() / 1.5f);
    this.gameObject.SetActive(false);
    //play particle and sound
}


   private float Time() 
    { 
        RuntimeAnimatorController ac = m_Animator.runtimeAnimatorController;    //Get Animator controller
        for(int i = 0; i<ac.animationClips.Length; i++)                 //For all animations
        {
            if(ac.animationClips[i].name == "Die")        //If it has the same name as your clip
            {
                return ac.animationClips[i].length;
            }
        }
        return 0.1f;
    }
    
 public void OnEnemyTurn() 
 {
     
     Attack();
     m_VisualEnemy.UpdateAttackUI(m_EnemyData.attackDamage[GameManager.Instance.TurnManager.turnCount]);
     
     Debug.Log("turnCount: " + GameManager.Instance.TurnManager.turnCount);
 }
 protected virtual void Attack()
 {
     m_AttackAnimation = "Melee Right Attack 01";
     
     StartCoroutine(DealingDamageToPlayer());
 }
 
 //Playing animation while the enemy is attacking and dealing damage to the player at the end of the animation
 //Also notifying the Turn Manager that the turn is over 
//https://answers.unity.com/questions/692593/get-animation-clip-length-using-animator.html
 IEnumerator DealingDamageToPlayer() {
     
     AnimatorStateInfo animInfo;
     m_Animator.SetTrigger(m_AttackAnimation);
     animInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
     
     yield return new WaitForSeconds(animInfo.length);

     int turnCount = GameManager.Instance.TurnManager.turnCount;
     if (turnCount < m_EnemyData.attackDamage.Length) {
         GameManager.Instance.Player.TakeDamage(m_EnemyData.attackDamage[turnCount]);
     }
     else {
         GameManager.Instance.TurnManager.turnCount = 0;
     }
     
     
     GameManager.Instance.TurnManager.EndEnemyTurn();
     m_VisualEnemy.UpdateAttackUI(m_EnemyData.attackDamage[GameManager.Instance.TurnManager.turnCount]);
 }


}
