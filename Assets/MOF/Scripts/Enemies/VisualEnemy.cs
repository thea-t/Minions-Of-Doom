using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisualEnemy : MonoBehaviour
{
   
    public GameObject selectionParticle;
    public GameObject cardSnapPoint;

    [SerializeField] private TextMeshProUGUI m_HealthTMP;
    [SerializeField] private TextMeshProUGUI m_AttackTMP;

    // Update is called once per frame
    void Start()
    {
    }

    public void UpdateHealthUI(int amount)
    {
        m_HealthTMP.text = "Health: " + amount;
    }

    public void UpdateAttackUI(int amount)
    {
        m_AttackTMP.text = "Damage: " + amount;
    }
}
