using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/// <summary>
/// The idea behind the turn manager is to create events which are listened by the Enemies and the Card Manager
/// </summary>
public class TurnManager : MonoBehaviour
{
    public event Action EnemyTurn;
    public event Action PlayerTurn;

    public int TurnCount { get; set; }

    /// <summary>
    /// Start the game coroutine.
    /// </summary>
    void Start()
    {
        TurnCount = 0;

        StartCoroutine(StartGameIn(3));
    }

    /// <summary>
    /// This is called when the player ends their turn.
    /// </summary>
    public void EndPlayerTurn()
    {
        EnemyTurn?.Invoke();
    }

    /// <summary>
    /// This is called when the enemy finishes its move.
    /// </summary>
    public void EndEnemyTurn()
    {
        TurnCount++;
        PlayerTurn?.Invoke();
    }

    /// <summary>
    /// Starts the game after delay.
    /// </summary>
    private IEnumerator StartGameIn(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayerTurn?.Invoke();
    }

}