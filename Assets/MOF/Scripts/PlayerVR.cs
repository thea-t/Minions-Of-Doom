using System.Collections;
using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

public class PlayerVR : MonoBehaviour
{
    private InputBridge m_InputBridge;
    private PlayerGravity m_PlayerGravity;
    public CapsuleCollider leftHandPointCollider;
    public CapsuleCollider rightHandPointCollider;

    public void AllowPlayerToInteractWithDoors()
    {
        leftHandPointCollider.isTrigger = false;
        rightHandPointCollider.isTrigger = false;
    }
    private void Start()
    {
        m_InputBridge = GetComponent<InputBridge>();
        m_PlayerGravity = GetComponentInChildren<PlayerGravity>();
        
        m_PlayerGravity.GravityEnabled = false;
        m_InputBridge.enabled = false;
        leftHandPointCollider.isTrigger = true;
        rightHandPointCollider.isTrigger = true;
    }
    
    public void EnterScene(Vector3 destination, int duration)
    {
        transform.DOMove(destination, duration).onComplete = () => 
        {
            m_PlayerGravity.GravityEnabled = true;
            m_InputBridge.enabled = true;
        };
    }
}
