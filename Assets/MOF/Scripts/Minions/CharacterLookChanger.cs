using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLookChanger : MonoBehaviour {
    
    [SerializeField] private CharacterLookData m_CharacterLookData;
    
    [Header("Transforms")]
    [SerializeField] private SkinnedMeshRenderer m_MeshRenderer;
    [SerializeField] private Transform m_HeadTransform;
    [SerializeField] private Transform m_BackTransform;
    [SerializeField] private Transform m_LeftHandTransform;
    [SerializeField] private Transform m_RightHandTransform;
    
    public void SetCharacterLook()
    {
        if (m_MeshRenderer.material != null)
        {
            m_MeshRenderer.material = m_CharacterLookData.minionMaterial;
        }
        else
        {
            Debug.LogError("Warning! Skin material is null for: " + gameObject.name);
        }


        if (m_CharacterLookData.head.Length > 0)
        {
            for (int i = 0; i < m_CharacterLookData.head.Length; i++)
            {
                Instantiate(m_CharacterLookData.head[i], m_HeadTransform);
            }
        }

        if (m_CharacterLookData.back != null)
        {
            Instantiate(m_CharacterLookData.back, m_BackTransform);
        }
        if (m_CharacterLookData.leftHand != null)
        {
            Instantiate(m_CharacterLookData.leftHand, m_LeftHandTransform);
        }
        if (m_CharacterLookData.rightHand != null)
        {

            Instantiate(m_CharacterLookData.rightHand, m_RightHandTransform);
        }
    }
}
