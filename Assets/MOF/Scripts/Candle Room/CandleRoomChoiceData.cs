using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CandleRoomChoiceData : ScriptableObject
{
    public string questionLineOne;
    public string questionLineTwo;
    public List<Choice> choices;
}
