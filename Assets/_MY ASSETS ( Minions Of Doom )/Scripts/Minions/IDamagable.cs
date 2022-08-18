using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface used by every object that has health and can be damaged
/// </summary>
public interface IDamagable
{
    int Block { get; set; }
    void TakeDamage(int amount);
    void Die();
}
