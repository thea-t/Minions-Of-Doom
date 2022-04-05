using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask selectableLayer;
    
    public RaycastManager raycastManager;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        raycastManager = gameObject.AddComponent<RaycastManager>();
        raycastManager.SetLayer(selectableLayer);
    }

}
