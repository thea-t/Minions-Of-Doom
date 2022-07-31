using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool vrFree;
    
    [SerializeField] private LayerMask selectableLayer;
    public PlayerCharacter Player;
    public UIManager UiManager;
    public TurnManager TurnManager;
    public DeckManager DeckManager;
    public LevelLauncher LevelLauncher;
    public AudioManager AudioManager;
    public ManaManager ManaManager;
    public RewardManager RewardManager;
    public EnemyManager EnemyManager;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
//Finding and assigning components when the script is reset. It's faster than dragging and dropping them individually
    void Reset()
    {
        Player = FindObjectOfType<PlayerCharacter>();
        DeckManager = GetComponent<DeckManager>();
        TurnManager = GetComponent<TurnManager>();
        UiManager = GetComponent<UIManager>();
        LevelLauncher = GetComponent<LevelLauncher>();
        AudioManager = GetComponent<AudioManager>();
        ManaManager = GetComponent<ManaManager>();
        EnemyManager = GetComponent<EnemyManager>();
    }

}
