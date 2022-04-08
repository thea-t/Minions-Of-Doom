using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayer;
    public Player Player;
    public RaycastManager RaycastManager;
    public CardManager CardManager;
    public TurnManager TurnManager;
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
    }

}
