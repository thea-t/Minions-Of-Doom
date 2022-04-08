using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemyData m_EnemyData;
    [SerializeField] private Animator m_Animator;

    protected string m_AttackAnimation;
    protected string m_BuffAnimation;
    
 public float MaxHealth { get; set; }
 public float CurrentHealth { get; set; }
 public float Block { get; set; }

 void Reset()
 {
     m_Animator = GetComponent<Animator>();
 }
 void Start()
 {
     GameManager.Instance.TurnManager.EnemyTurn += Attack;
 }
 public void TakeDamage(float amount)
 {
     throw new System.NotImplementedException();
 }

 public void Die()
 {
     throw new System.NotImplementedException();
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
     
     GameManager.Instance.Player.TakeDamage(m_EnemyData.attackDamage);

     GameManager.Instance.TurnManager.EndEnemyTurn();
 }
}
