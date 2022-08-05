using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MinionData : ScriptableObject
{
    // Settings
    public string name;
    public string description;
    public int cost;
    public bool isMale;
    public int damage;
    public int block;
    public int strength;
    public MinionRarity minionRarity;
    
    //Spells
    public bool exhausts;
    
}

public enum MinionRarity
{
    None,
    Common,
    Rare,
    Legendary
}
