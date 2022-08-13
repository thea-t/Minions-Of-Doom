using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamagable
{ 
[SerializeField] private int m_StartingHealth;
//[SerializeField] private GameObject m_CharacterPrefab;
[SerializeField][Range(0,10)] private int m_CardsToDrawOnStart;
[SerializeField] private GameObject m_DeadPanel;
[SerializeField] private Button m_MenuButton;
[SerializeField] private Button m_ResetButton;
public GameObject centerEye;
    
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int Strength { get; set; }
    public int Block { get; set; }
    public int CardsToDrawOnStart { get; set; }
    
   //Setting player's health and updating its UI
    void Start() {
        
        CardsToDrawOnStart = m_CardsToDrawOnStart;
        MaxHealth = m_StartingHealth;
        CurrentHealth = MaxHealth;
        Strength = 0;
        Block = 0;
        GameManager.Instance.UiManager.UpdatePlayerHealth(CurrentHealth);
        GameManager.Instance.UiManager.UpdateBlockUI(Block);
        GameManager.Instance.UiManager.UpdateStrengthUI(Strength);
        m_MenuButton.onButtonDown.AddListener(OnMenuButtonPressed);
        m_ResetButton.onButtonDown.AddListener(OnResetButtonButtonPressed);
        
        m_DeadPanel.transform.DOScale(Vector3.zero, 0);
    }
    
    //Reducing player's health and updating its UI when the enemy takes damage
    public void TakeDamage(int amount)
    {
        if (CurrentHealth > 0) {
            CurrentHealth -= amount + Block;
            GameManager.Instance.UiManager.UpdatePlayerHealth(CurrentHealth);
        }
        else {
            Die();
        }
    }


    public void Die()
    {
        StartCoroutine(Dying());
    }

    IEnumerator Dying()
    {
        //play heartbeat sounds
        yield return new WaitForSeconds(3);
        m_DeadPanel.transform.DOScale(Vector3.one, 5);
    }

    private void OnMenuButtonPressed()
    {
        SceneLoader.FadeToScene("Menu");
    }
    
    private void OnResetButtonButtonPressed()
    {
        Player.CurrentLevel = 0;
        Player.WonMinions.Clear();
        SceneLoader.FadeToScene("Map");
    }
}
