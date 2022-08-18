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

    private void Start()
    {
        GameManager.Instance.DeckManager.MinionsDrawned += RaiseButton;

        m_JointHelper = m_InnerButton.GetComponent<JointHelper>();

        m_InnerButton.onButtonDown.AddListener(OnButtonPressed);

    }

    /// <summary>
    /// This is called when the end turn button is pressed. It animates the button, locks the position and ends the players turn.
    /// </summary>
    private void OnButtonPressed()
    {
        GameManager.Instance.TurnManager.EndPlayerTurn();
        m_InnerButton.transform.DOMoveY(BUTTON_DOWN_POSY, 0.2f).onComplete += () =>
        {
            m_JointHelper.LockYPosition = true;
            m_Collider.isTrigger = true;
        };
    }

    /// <summary>
    /// This is called after the end turn button is released. It unlocks the position of the button.
    /// </summary>
    private void RaiseButton()
    {
        if (m_Collider)
        {

            m_JointHelper.LockYPosition = false;
            m_Collider.isTrigger = false;
        }
    }
}
