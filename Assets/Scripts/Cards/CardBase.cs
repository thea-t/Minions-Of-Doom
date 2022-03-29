using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base card class that holds common card functionalities.
/// </summary>
public class CardBase : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private Texture2D image;
}
