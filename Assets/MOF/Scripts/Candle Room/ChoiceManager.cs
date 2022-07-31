using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private PlayerVR m_Player;
    [SerializeField] private CandleRoomChoiceData[] m_ChoiceData;
    [SerializeField] private ChoiceUi m_ChoiceUi;

    private void Start()
    {
        m_Player.EnterScene(new Vector3(0, -0.5f, 0), 8);
        CandleRoomChoiceData currentChoiceData = m_ChoiceData[Random.Range(0, m_ChoiceData.Length)];
        List<Choice> choices = currentChoiceData.choices;

        Choice choiceOne = choices[Random.Range(0, choices.Count)];
        choices.Remove(choiceOne);
        Choice choiceTwo = choices[Random.Range(0, choices.Count)];

        if (currentChoiceData && choiceOne && choiceTwo)
        {
            SetUi(currentChoiceData, choiceOne, choiceTwo);
            choices.Add(choiceOne);
        }

        m_ChoiceUi.buttonOptionOne.onClick.AddListener(choiceOne.OnExecute);
        m_ChoiceUi.buttonOptionTwo.onClick.AddListener(choiceTwo.OnExecute);
        choiceOne.choiceSelected += ChoiceSelected;
        choiceTwo.choiceSelected += ChoiceSelected;

        StartCoroutine(mockup(choiceOne));
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
        float duration = 1;
        m_ChoiceUi.lineOneTMP.DOFade(0, duration);
        m_ChoiceUi.lineTwoTMP.DOFade(0, duration);
        m_ChoiceUi.buttonOptionOne.transform.DOScale(Vector3.zero, duration);
        m_ChoiceUi.buttonOptionTwo.transform.DOScale(Vector3.zero, duration);
        m_ChoiceUi.question.transform.DOScale(Vector3.zero, duration).onComplete = () =>
            m_ChoiceUi.choiceSelected.transform.DOScale(Vector3.one, 5).onComplete = () => SceneLoader.FadeToScene("Map");
    }

    IEnumerator mockup(Choice choice)
    {
        yield return new WaitForSeconds(10);
        choice.choiceSelected?.Invoke();
    }
}