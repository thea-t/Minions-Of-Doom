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

   
    /// <summary>
    /// Sets the players health text ui
    /// </summary>
    public void UpdatePlayerHealth(int health, int maxHealth)
    {
        m_PlayerHealthTMP.text = "Health: " + health + " / " + maxHealth;
    }

    /// <summary>
    /// Sets the players mana text ui
    /// </summary>
    public void UpdateManaUI(int amount)
    {
        m_ManaTMP.text = "Mana: " + amount;
    }

    /// <summary>
    /// Sets the players strength text ui
    /// </summary>
    public void UpdateStrengthUI(int amount)
    {
        m_StrengthTMP.text = "Strength: " + amount;
    }

    /// <summary>
    /// Sets the players block text ui
    /// </summary>
    public void UpdateBlockUI(float amount)
    {
        m_BlockTMP.text = "Shield: " + amount;
    }
}