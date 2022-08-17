using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamagable
{ 
[SerializeField][Range(0,10)] private int m_CardsToDrawOnStart;
[SerializeField] private GameObject m_DeadPanel;
public GameObject centerEye;
    
    public int Strength { get; set; }
    public int MaxHealth { get; set; }
    public int Block { get; set; }
    public int CardsToDrawOnStart { get; set; }
    
   //Setting player's health and updating its UI
    void Start() {
        
        CardsToDrawOnStart = m_CardsToDrawOnStart;
        Player.MaxHealth = MaxHealth;
        Strength = Player.StartingStrength;
        Block = Player.StartingShield;
        GameManager.Instance.UiManager.UpdatePlayerHealth(Player.CurrentHealth);
        GameManager.Instance.UiManager.UpdateBlockUI(Block);
        GameManager.Instance.UiManager.UpdateStrengthUI(Strength);
        
        m_DeadPanel.transform.DOScale(Vector3.zero, 0);
        //StartCoroutine(TestDie());
    }
    
    private IEnumerator TestDie()
    {
        yield return new WaitForSeconds(5);
        Die();
    }
    
    //Reducing player's health and updating its UI when the enemy takes damage
    public void TakeDamage(int amount)
    {
        if (Player.CurrentHealth > 0) {
            Player.CurrentHealth -= amount + Block;
            GameManager.Instance.UiManager.UpdatePlayerHealth(Player.CurrentHealth);
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

    //Called OnClick() from their button components in the scene
    public void OnMenuButtonPressed()
    {
        StartCoroutine(SceneLoader.FadeToScene("Menu"));
        Debug.Log("OnMenuButtonPressed");
    }
    
    public void OnResetButtonButtonPressed()
    {
        Player.CurrentLevel = 0;
        Player.WonMinions.Clear();
        StartCoroutine(SceneLoader.FadeToScene("Intro"));
        Debug.Log("OnResetButtonButtonPressed");
    }
}
