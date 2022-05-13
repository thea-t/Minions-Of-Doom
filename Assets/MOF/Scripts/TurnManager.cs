using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
//OLD CODE THAT WAS CREATED WHILE THE TARGET PLATFORM WAS STILL MOBILE
//NEEDS REWORK AS MOST OF IT IS DEPRECATED DUE TO THE VR INTERACTIONS

//The idea behind the turn manager is to create events which are listened by the Enemies and the Card Manager
public class TurnManager : MonoBehaviour
{
    [SerializeField] private Button m_EndTurnButton;
    public event Action EnemyTurn;
    public event Action PlayerTurn;

    void Start()
    {
        m_EndTurnButton.onClick.AddListener(EndPlayerTurn);
    }

    void EndPlayerTurn()
    {
        m_EndTurnButton.interactable = false;
        EnemyTurn?.Invoke();
    }

    public void EndEnemyTurn()
    {
        m_EndTurnButton.interactable = true;
        PlayerTurn?.Invoke();
    }
}