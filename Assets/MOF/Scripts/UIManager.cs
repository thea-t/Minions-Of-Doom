using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_PlayerHealthTMP;

    public void UpdatePlayerHealth(int amount)
    {
        m_PlayerHealthTMP.text = "Health: " + amount;
    }
}
