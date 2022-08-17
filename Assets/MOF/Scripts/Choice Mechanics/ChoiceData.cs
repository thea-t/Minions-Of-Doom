using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ChoiceData : ScriptableObject
{
    public string questionLineOne;
    public string questionLineTwo;
    public List<Choice> choices;
}
