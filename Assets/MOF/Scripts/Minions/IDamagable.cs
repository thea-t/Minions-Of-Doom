using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface used by every object that has health and can be damaged
public interface IDamagable
{
    int MaxHealth { get; set; }
    int Block { get; set; }
    

    void TakeDamage(int amount);
    void Die();
}
