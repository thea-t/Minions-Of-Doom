using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

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