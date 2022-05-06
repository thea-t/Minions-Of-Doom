using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    [SerializeField] private VisualMap m_VisualMap;
    [SerializeField] private FloorData[] floors;
    
    private int currentFloor = 0;


    private void Reset() {
        m_VisualMap = GetComponent<VisualMap>();
    }

    private void Start() {
        GenerateDoors(floors[currentFloor].doorCount);
    }

    private void GenerateDoors(int doorCount) {
        
    }
}
