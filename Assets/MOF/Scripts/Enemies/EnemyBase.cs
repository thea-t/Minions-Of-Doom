using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase :  MonoBehaviour, IDamagable
{
    public VisualEnemy visualEnemy;
    [SerializeField] private EnemyData m_EnemyData;
    [SerializeField] private Animator m_Animator;

    protected string m_AttackAnimation;
    protected string m_BuffAnimation;

    
 public int MaxHealth { get; set; }
 public int CurrentHealth { get; set; }
 public float Block { get; set; }

 private int m_TurnCount;
 void Reset()
 {
     m_Animator = GetComponent<Animator>();
     visualEnemy = GetComponent<VisualEnemy>();
 }
 void Start()
 {
     
     m_EnemyData.health = m_EnemyData.maxHealth;
     MaxHealth = m_EnemyData.maxHealth;
     CurrentHealth = m_EnemyData.health;
     
     m_TurnCount = 0;
     visualEnemy.UpdateHealthUI(CurrentHealth);
     visualEnemy.UpdateAttackUI(m_EnemyData.attackDamage[m_TurnCount]);
     
     GameManager.Instance.TurnManager.EnemyTurn += Attack;
 }

 public void TakeDamage(int amount)
 {
     CurrentHealth -= amount;
     visualEnemy.UpdateHealthUI(CurrentHealth);
 }

 public void Die()
 {
     
 }
 
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
     
     GameManager.Instance.Player.TakeDamage(m_EnemyData.attackDamage[m_TurnCount]);

     GameManager.Instance.TurnManager.EndEnemyTurn();
     OnTurnOver();
 }

 public void HoveringWithCard(bool hovering)
 {
     visualEnemy.selectionParticle.SetActive(hovering);
 }

 private void OnTurnOver()
 {
     m_TurnCount++;
     visualEnemy.UpdateAttackUI(m_EnemyData.attackDamage[m_TurnCount]);
     
     Debug.Log("Turn Count: " + m_TurnCount);
 }
}
