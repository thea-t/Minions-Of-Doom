using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//This class is responsible for all the UI and visual elements of the enemy and setting their values
public class VisualEnemy : MonoBehaviour
{
   
    public GameObject selectionParticle; // used by the Enemy (See EnemyBase.cs) to indicate that an enemy is selected
    public GameObject cardSnapPoint; // used by the Cards (see CardBase.cs) to snap the card on top of an enemy and indicate that this enemy is being selected

    [SerializeField] private TextMeshProUGUI m_HealthTMP;
    [SerializeField] private TextMeshProUGUI m_AttackTMP;


    public void UpdateHealthUI(int amount)
    {
        m_HealthTMP.text = "Health: " + amount;
    }

    public void UpdateAttackUI(int amount)
    {
        m_AttackTMP.text = "Damage: " + amount;
    }
}
