using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object that holds a conversations choice data.
/// </summary>
[CreateAssetMenu]
public class ChoiceData : ScriptableObject
{
    public string questionLineOne;
    public string questionLineTwo;
    public List<Choice> choices;
}
