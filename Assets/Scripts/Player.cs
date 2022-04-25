using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour, IDamagable
{
    [SerializeField] private int m_StartingHealth;
    
    
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public float Block { get; set; }
    public int CardsToDrawOnStart { get; set; } = 4;
    public int CardToDrawOnEveryTurn{ get; set; }
    private int Gold;
    private int Energy;

    void Start()
    {
        MaxHealth = m_StartingHealth;
        CurrentHealth = MaxHealth;
        GameManager.Instance.UiManager.UpdatePlayerHealth(CurrentHealth);
    }
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
