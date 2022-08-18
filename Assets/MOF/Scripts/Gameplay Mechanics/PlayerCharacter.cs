using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamagable
{
    [SerializeField] [Range(0, 10)] private int m_CardsToDrawOnStart;
    [SerializeField] private GameObject m_DeadPanel;
    
    public GameObject centerEye;

    public int Strength { get; set; }
    public int MaxHealth { get; set; }
    public int Block { get; set; }
    public int CardsToDrawOnStart { get; set; }
    
    /// <summary>
    /// Setting player's health and updating its UI
    /// </summary>
    private void Start()
    {
        CardsToDrawOnStart = m_CardsToDrawOnStart;
        Player.MaxHealth = MaxHealth;
        Strength = Player.StartingStrength;
        Block = Player.StartingShield;
        GameManager.Instance.UiManager.UpdatePlayerHealth(Player.CurrentHealth);
        GameManager.Instance.UiManager.UpdateBlockUI(Block);
        GameManager.Instance.UiManager.UpdateStrengthUI(Strength);

        m_DeadPanel.transform.DOScale(Vector3.zero, 0);
    }

    /// <summary>
    /// This is called when the player takes damage. It reduces player's health and updating its UI
    /// </summary>
    public void TakeDamage(int amount)
    {
        if (Player.CurrentHealth > 0)
        {
            Player.CurrentHealth -= amount + Block;
            GameManager.Instance.UiManager.UpdatePlayerHealth(Player.CurrentHealth);
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(Dying());
    }

    /// <summary>
    /// Coroutine that scales the minion down after some time.
    /// </summary>
    /// <returns></returns>
    IEnumerator Dying()
    {
        yield return new WaitForSeconds(3);
        m_DeadPanel.transform.DOScale(Vector3.one, 5);
    }

    /// <summary>
    /// Button event. It changes the scene.
    /// </summary>
    public void OnMenuButtonPressed()
    {
        StartCoroutine(SceneLoader.FadeToScene("Menu"));
    }

    /// <summary>
    /// This resets the players stats and returns to the main scene.
    /// </summary>
    public void OnResetButtonButtonPressed()
    {
        Player.CurrentLevel = 0;
        Player.StartingMana = 5;
        Player.StartingShield = 0;
        Player.StartingStrength = 0;
        Player.CurrentHealth = Player.MaxHealth;
        Player.WonMinions.Clear();
        StartCoroutine(SceneLoader.FadeToScene("Intro"));
    }
}