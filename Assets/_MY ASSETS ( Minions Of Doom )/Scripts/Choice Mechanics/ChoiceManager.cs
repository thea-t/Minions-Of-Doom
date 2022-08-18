using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// This class controls the choices.
/// </summary>
public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private ChoiceData[] m_ChoiceData;
    [SerializeField] private ChoiceUi m_ChoiceUi;

    private Choice m_ChoiceOne;
    private Choice m_ChoiceTwo;

    /// <summary>
    /// Initialize the choices by picking a random choice and setting the popup window's ui
    /// </summary>
    private void Start()
    {
        ChoiceData currentChoiceData = m_ChoiceData[Random.Range(0, m_ChoiceData.Length)];
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
    }

    /// <summary>
    /// First choice button event
    /// </summary>
    public void OnChoiceOneSelected()
    {
        m_ChoiceOne.OnExecute();
    }

    /// <summary>
    /// Second choice button event
    /// </summary>
    public void OnChoiceOTwoSelected()
    {
        m_ChoiceTwo.OnExecute();
    }

    /// <summary>
    /// Sets the ui of choices popup
    /// </summary>
    private void SetUi(ChoiceData data, Choice choiceOne, Choice choiceTwo)
    {
        m_ChoiceUi.lineOneTMP.text = data.questionLineOne;
        m_ChoiceUi.lineTwoTMP.text = data.questionLineTwo;
        m_ChoiceUi.buttonOneTMP.text = choiceOne.buttonDesc;
        m_ChoiceUi.buttonTwoTMP.text = choiceTwo.buttonDesc;
    }

    /// <summary>
    /// This is called when a choice is selected. It animates the choices popup and switches the map scene.
    /// </summary>
    private void ChoiceSelected()
    {
        float duration = 1;
        m_ChoiceUi.lineOneTMP.DOFade(0, duration);
        m_ChoiceUi.lineTwoTMP.DOFade(0, duration);
        m_ChoiceUi.buttonOptionOne.transform.DOScale(Vector3.zero, duration);
        m_ChoiceUi.buttonOptionTwo.transform.DOScale(Vector3.zero, duration);
        m_ChoiceUi.question.transform.DOScale(Vector3.zero, duration).onComplete = () =>
            m_ChoiceUi.choiceSelected.transform.DOScale(Vector3.one, 5).onComplete =
                () => StartCoroutine(SceneLoader.FadeToScene("Map"));
    }
}