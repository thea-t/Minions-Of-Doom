using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterLookData : ScriptableObject
{

    [Header("Accessories and look")] 
    
    public Material minionMaterial;

    public GameObject[] head;
    public GameObject back;
    
    [Header("It's recommended to place melee weapon on right, secondary on left")]
    public GameObject rightHand;
    public  GameObject leftHand;
    
}
