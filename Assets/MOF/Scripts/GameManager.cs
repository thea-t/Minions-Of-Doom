using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayer;
    public RaycastManager RaycastManager;
    public PlayerCharacter Player;
    public UIManager UiManager;
    public TurnManager TurnManager;
    public CardManager CardManager;
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
        Player = FindObjectOfType<PlayerCharacter>();
        RaycastManager = GetComponent<RaycastManager>();
        CardManager = GetComponent<CardManager>();
        TurnManager = GetComponent<TurnManager>();
        UiManager = GetComponent<UIManager>();
        LevelLauncher = GetComponent<LevelLauncher>();
    }

}
