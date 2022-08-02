using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

public class EndTurnButtonController : MonoBehaviour
{
    [SerializeField] private BNG.Button m_InnerButton;
    [SerializeField] private BoxCollider m_Collider;
    private const float BUTTON_DOWN_POSY = 0.5f;

    private JointHelper m_JointHelper;

    void Start() { ;
        GameManager.Instance.DeckManager.MinionsDrawned += RaiseButton;

        m_JointHelper = m_InnerButton.GetComponent<JointHelper>();
        
        m_InnerButton.onButtonDown.AddListener(OnButtonPressed);

    }
    
    private void OnButtonPressed() {
        GameManager.Instance.TurnManager.EndPlayerTurn();
        Debug.Log("btn down");
        m_InnerButton.transform.DOMoveY(BUTTON_DOWN_POSY, 0.2f).onComplete += () =>
        {
            m_JointHelper.LockYPosition = true;
            m_Collider.isTrigger = true;
        };
    }

    private void RaiseButton() {
        if (m_Collider) {
            
            Debug.Log("btn up");
            m_JointHelper.LockYPosition = false;
            m_Collider.isTrigger = false;
        }
        else {
            Debug.Log("failed");

        }
    }
}
