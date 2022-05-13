using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamagable
{ 
[SerializeField] private int m_StartingHealth;
[SerializeField] private GameObject m_CharacterPrefab;
         
    
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public float Block { get; set; }
    public int CardsToDrawOnStart { get; set; } = 4;
    public int CardToDrawOnEveryTurn{ get; set; }
    
   //Setting player's health and updating its UI
    void Start()
    {
        MaxHealth = m_StartingHealth;
        CurrentHealth = MaxHealth;
        GameManager.Instance.UiManager.UpdatePlayerHealth(CurrentHealth);
    }
    
    //Reducing player's health and updating its UI when the enemy takes damage
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        Debug.Log("Damage taken! Current health: " + CurrentHealth);
        GameManager.Instance.UiManager.UpdatePlayerHealth(CurrentHealth);
    }


    public void Die()
    {
    }
}
