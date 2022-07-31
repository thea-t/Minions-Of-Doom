using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
//OLD CODE THAT WAS CREATED WHILE THE TARGET PLATFORM WAS STILL MOBILE
//NEEDS REWORK AS MOST OF IT IS DEPRECATED DUE TO THE VR INTERACTIONS

//The idea behind the turn manager is to create events which are listened by the Enemies and the Card Manager
public class TurnManager : MonoBehaviour
{
    [SerializeField] private BNG.Button m_EndTurnButton;
    public event Action EnemyTurn;
    public event Action PlayerTurn;

    public int turnCount { get; set; }
    void Start()
    {
        m_EndTurnButton.onButtonDown.AddListener(EndPlayerTurn);
        turnCount = 0;

        StartCoroutine(StartGameIn(3));
    }

    public void EndPlayerTurn() {
        EnemyTurn?.Invoke();
        Debug.Log("Enemy Turn");
    }

    public void EndEnemyTurn() {
        turnCount++;
        PlayerTurn?.Invoke();
        Debug.Log("Player Turn");
    }

    private IEnumerator StartGameIn( int seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayerTurn.Invoke();
    }

}