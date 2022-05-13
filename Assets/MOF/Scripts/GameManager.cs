using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayer;
    public PlayerCharacter Player;
    public UIManager UiManager;
    public TurnManager TurnManager;
    public CardManager CardManager;
    public LevelLauncher LevelLauncher;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
//Finding and assigning components when the script is reset. It's faster than dragging and dropping them individually
    void Reset()
    {
        Player = FindObjectOfType<PlayerCharacter>();
        CardManager = GetComponent<CardManager>();
        TurnManager = GetComponent<TurnManager>();
        UiManager = GetComponent<UIManager>();
        LevelLauncher = GetComponent<LevelLauncher>();
    }

}
