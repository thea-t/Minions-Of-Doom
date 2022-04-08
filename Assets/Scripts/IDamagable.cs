using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
    float Block { get; set; }
    

    void TakeDamage(float amount);
    void Die();
}
