using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour, IDamagable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public float Block { get; set; }
    public int CardsToDrawOnStart { get; set; } = 4;
    public int CardToDrawOnEveryTurn{ get; set; }
    private int Gold;
    private int Energy;

    void Start()
    {
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        Debug.Log("Damage taken! Current health: " + CurrentHealth);
    }

    public void Die()
    {
    }
}
