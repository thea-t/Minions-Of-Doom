using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayer;
    public RaycastManager RaycastManager;
    public Player Player;
    public UIManager UiManager;
    public TurnManager TurnManager;
    public CardManager CardManager;
    public EnemyManager EnemyManager;
    public LevelLauncher LevelLauncher;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        RaycastManager = gameObject.AddComponent<RaycastManager>();
        RaycastManager.SetLayer(selectableLayer);
    }

    void Reset()
    {
        Player = FindObjectOfType<Player>();
        RaycastManager = GetComponent<RaycastManager>();
        CardManager = GetComponent<CardManager>();
        TurnManager = GetComponent<TurnManager>();
        UiManager = GetComponent<UIManager>();
        EnemyManager = GetComponent<EnemyManager>();
        LevelLauncher = GetComponent<LevelLauncher>();
    }

}
