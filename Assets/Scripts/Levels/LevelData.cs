using UnityEngine;

/// <summary>
/// Scritable object that holds specific level data.
/// Level is a floor that contains either an enemy, shop, etc.
/// Has an enum of LevelType with Shop, Enemy, etc.
/// </summary>
[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public EnemyData[] enemies;
}

