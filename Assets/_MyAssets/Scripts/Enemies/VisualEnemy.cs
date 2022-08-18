using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// This class is responsible for all the UI and visual elements of the enemy and setting their values
/// </summary>
public class VisualEnemy : CharacterLookChanger
{
    // used by the Enemy (See EnemyBase.cs) to indicate that an enemy is selected
    public GameObject selectionParticle; 
    
    // used by the Cards (see CardBase.cs) to snap the card on top of an enemy and indicate that this enemy is being selected
    public GameObject cardSnapPoint;

    [SerializeField] private TextMeshProUGUI m_HealthTMP;
    [SerializeField] private TextMeshProUGUI m_AttackTMP;


    /// <summary>
    /// Sets the health text ui of the enemy
    /// </summary>
    public void UpdateHealthUI(int amount)
    {
        m_HealthTMP.text = "Health: " + amount;
    }
    
    /// <summary>
    /// Sets the attack text ui of the enemy
    /// </summary>
    public void UpdateAttackUI(int amount)
    {
        m_AttackTMP.text = "Damage: " + amount;
    }
}
