using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
    float Block { get; set; }
    

    void TakeDamage(int amount);
    void Die();
}
