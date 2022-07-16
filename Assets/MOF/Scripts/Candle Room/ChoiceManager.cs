using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private CandleRoomChoiceData[] m_ChoiceData;
    [SerializeField] private ChoiceUi m_ChoiceUi;


    private void Start()
    {
        CandleRoomChoiceData currentChoiceData = m_ChoiceData[Random.Range(0, m_ChoiceData.Length)];
        Choice choiceOne = currentChoiceData.choices[Random.Range(0, currentChoiceData.choices.Count)];
        currentChoiceData.choices.Remove(choiceOne);
        Choice choiceTwo = currentChoiceData.choices[Random.Range(0, currentChoiceData.choices.Count)];

        if (currentChoiceData && choiceOne && choiceTwo) SetUi(currentChoiceData, choiceOne, choiceTwo);
        
    }

    private void SetUi(CandleRoomChoiceData data, Choice choiceOne, Choice choiceTwo)
    {
        m_ChoiceUi.m_LineOneTMP.text = data.questionLineOne;
        m_ChoiceUi.m_LineTwoTMP.text = data.questionLineTwo;
        m_ChoiceUi.m_ButtonOneTMP.text = choiceOne.buttonDesc;
        m_ChoiceUi.m_ButtonTwoTMP.text = choiceTwo.buttonDesc;
    }

    private IEnumerator PickRandomChoices()
    { 
        yield return null;
    }
}
