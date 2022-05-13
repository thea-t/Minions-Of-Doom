using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase :  MonoBehaviour, IDamagable
{
    public VisualEnemy visualEnemy;
    
    public EnemyData enemyData;
    
    [SerializeField] private Animator m_Animator;

    protected string m_AttackAnimation;
    protected string m_BuffAnimation;

    
 public int MaxHealth { get; set; }
 public int CurrentHealth { get; set; }
 public float Block { get; set; }

 private int m_TurnCount;
 
 //Getting and assigning components on Reset in order to save time from dragging and dropping them
 void Reset()
 {
     m_Animator = GetComponent<Animator>();
     visualEnemy = GetComponent<VisualEnemy>();
 }
 
 //Setting the enemy's health and updating its UI.
 //Resetting the turn count as this int is used for setting different attack values each turn
 //Subscribing to TurnManager's event and listening for the enemy's turn in order to attack the player
 
 void Start()
 {
     MaxHealth = enemyData.maxHealth;
     CurrentHealth = enemyData.maxHealth;
     
     m_TurnCount = 0;
     visualEnemy.UpdateHealthUI(CurrentHealth);
     visualEnemy.UpdateAttackUI(enemyData.attackDamage[m_TurnCount]);
     
     GameManager.Instance.TurnManager.EnemyTurn += Attack;
 }

 //Reducing enemy's health and updating its UI when the enemy takes damage
 public void TakeDamage(int amount)
 {
     CurrentHealth -= amount;
     visualEnemy.UpdateHealthUI(CurrentHealth);
 }

 public void Die()
 {
     
 }
 
 //Playing animation while the enemy is attacking and dealing damage to the player at the end of the animation
 //Also notifying the Turn Manager that the turn is over 
//https://answers.unity.com/questions/692593/get-animation-clip-length-using-animator.html
 protected virtual void Attack()
 {
     m_AttackAnimation = "Melee Right Attack 01";
     m_BuffAnimation = "";
     
     StartCoroutine(DealDamageToPlayer());
 }

 IEnumerator DealDamageToPlayer()
 { 
     AnimatorStateInfo animInfo;
     
     

     m_Animator.SetTrigger(m_AttackAnimation);
     animInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
     
     yield return new WaitForSeconds(animInfo.length);
     
     GameManager.Instance.Player.TakeDamage(enemyData.attackDamage[m_TurnCount]);

     GameManager.Instance.TurnManager.EndEnemyTurn();
     OnTurnOver();
 }

 //Setting active an selection particle when the enemy was hovered over
 public void HoveringWithCard(bool hovering)
 {
     visualEnemy.selectionParticle.SetActive(hovering);
 }

 //Increasing the turn count and updating enemy's UI as enemy deals different attack damage every turn
 private void OnTurnOver()
 {
     m_TurnCount++;
     visualEnemy.UpdateAttackUI(enemyData.attackDamage[m_TurnCount]);
     
     Debug.Log("Turn Count: " + m_TurnCount);
 }
}
