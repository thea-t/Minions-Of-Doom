using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //This class is responsible for all the UI and visual elements of the player
    [SerializeField] private TextMeshProUGUI m_PlayerHealthTMP;
    [SerializeField] private TextMeshProUGUI m_ManaTMP;
    [SerializeField] private TextMeshProUGUI m_StrengthTMP;
    [SerializeField] private TextMeshProUGUI m_BlockTMP;
    
    //Updating player's UI based on their health
    public void UpdatePlayerHealth(int amount)
    {
        m_PlayerHealthTMP.text = "Health: " + amount;
    }

    public void UpdateManaUI(int amount)
    {
        m_ManaTMP.text = "Mana: " + amount;
    }
    public void UpdateStrengthUI(int amount)
    {
        m_StrengthTMP.text = "Strength: " + amount;
    }
    public void UpdateBlockUI(int amount)
    {
        m_BlockTMP.text = "Shield: " + amount;
    }
}
