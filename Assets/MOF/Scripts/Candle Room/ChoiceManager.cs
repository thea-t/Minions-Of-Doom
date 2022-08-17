using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private CandleRoomChoiceData[] m_ChoiceData;
    [SerializeField] private ChoiceUi m_ChoiceUi;

    [SerializeField] private bool m_Debug;

    private Choice m_ChoiceOne;
    private Choice m_ChoiceTwo;
    private void Start()
    {
        CandleRoomChoiceData currentChoiceData = m_ChoiceData[Random.Range(0, m_ChoiceData.Length)];
        List<Choice> choices = currentChoiceData.choices;

        Choice choiceOne = choices[Random.Range(0, choices.Count)];
        m_ChoiceOne = choiceOne;
        choices.Remove(choiceOne);
        
        Choice choiceTwo = choices[Random.Range(0, choices.Count)];
        m_ChoiceTwo = choiceTwo;
        
        if (currentChoiceData && choiceOne && choiceTwo)
        {
            SetUi(currentChoiceData, choiceOne, choiceTwo);
            choices.Add(choiceOne);
        }

        m_ChoiceUi.buttonOptionOne.onClick.AddListener(choiceOne.OnExecute);
        m_ChoiceUi.buttonOptionTwo.onClick.AddListener(choiceTwo.OnExecute);
        choiceOne.choiceSelected += ChoiceSelected;
        choiceTwo.choiceSelected += ChoiceSelected;

        if (m_Debug) {
            StartCoroutine(mockup(choiceOne));
        }
    }

    //button events
    public void OnChoiceOneSelected() {
        m_ChoiceOne.OnExecute();
        Debug.Log("executed");
    }
    public void OnChoiceOTwoSelected() {
        m_ChoiceTwo.OnExecute();
        Debug.Log("executed");
    }

    private void SetUi(CandleRoomChoiceData data, Choice choiceOne, Choice choiceTwo)
    {
        m_ChoiceUi.lineOneTMP.text = data.questionLineOne;
        m_ChoiceUi.lineTwoTMP.text = data.questionLineTwo;
        m_ChoiceUi.buttonOneTMP.text = choiceOne.buttonDesc;
        m_ChoiceUi.buttonTwoTMP.text = choiceTwo.buttonDesc;
    }

    private void ChoiceSelected()
    {
        Debug.Log("trying to fade");
        
        float duration = 1;
        m_ChoiceUi.lineOneTMP.DOFade(0, duration);
        m_ChoiceUi.lineTwoTMP.DOFade(0, duration);
        m_ChoiceUi.buttonOptionOne.transform.DOScale(Vector3.zero, duration);
        m_ChoiceUi.buttonOptionTwo.transform.DOScale(Vector3.zero, duration);
        m_ChoiceUi.question.transform.DOScale(Vector3.zero, duration).onComplete = () =>
            m_ChoiceUi.choiceSelected.transform.DOScale(Vector3.one, 5).onComplete = () => StartCoroutine(SceneLoader.FadeToScene("Map"));
    }

    IEnumerator mockup(Choice choice)
    {
        yield return new WaitForSeconds(10);
        choice.choiceSelected?.Invoke();
    }
}