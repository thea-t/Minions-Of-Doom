using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamagable
{ 
[SerializeField] private int m_StartingHealth;
//[SerializeField] private GameObject m_CharacterPrefab;
[SerializeField][Range(0,10)] private int m_CardsToDrawOnStart;
public GameObject centerEye;
    
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int Strength { get; set; }
    public float Block { get; set; }
    public int CardsToDrawOnStart { get; set; }
    
   //Setting player's health and updating its UI
    void Start() {
        CardsToDrawOnStart = m_CardsToDrawOnStart;
        MaxHealth = m_StartingHealth;
        CurrentHealth = MaxHealth;
        Strength = 0;
        Block = 0;
        GameManager.Instance.UiManager.UpdatePlayerHealth(CurrentHealth);
        GameManager.Instance.UiManager.UpdateBlockUI(Block);
        GameManager.Instance.UiManager.UpdateStrengthUI(Strength);
    }
    
    //Reducing player's health and updating its UI when the enemy takes damage
    public void TakeDamage(int amount)
    {
        if (CurrentHealth >0) {
            CurrentHealth -= amount + (int)Block;
            Debug.Log("Damage taken! Current health: " + CurrentHealth);
            GameManager.Instance.UiManager.UpdatePlayerHealth(CurrentHealth);
        }
        else {
            Die();
        }
    }


    public void Die()
    { 
        Debug.Log("DEAD");
    }
}
