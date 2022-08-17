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


    // Adds this function to the menu options so that you can call it in the editor. This way you can check its look 
    [ContextMenu("Set look")]
    public void SetCharacterLook()
    {
        if (m_MeshRenderer.sharedMaterial != null)
        {
            m_MeshRenderer.sharedMaterial = m_CharacterLookData.minionMaterial;
        }
        else
        {
            Debug.LogError("Warning! Skin material is null for: " + gameObject.name);
        }

        ClearPrevious();


        if (m_CharacterLookData.head.Length > 0)
        {
            for (int i = 0; i < m_CharacterLookData.head.Length; i++)
            {
                if (m_CharacterLookData.head[i] != null)
                {
                    Instantiate(m_CharacterLookData.head[i], m_HeadTransform);
                }
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

    private void ClearPrevious()
    {
        foreach (Transform child in m_HeadTransform)
        {
            DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in m_BackTransform)
        {
            DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in m_LeftHandTransform)
        {
            DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in m_RightHandTransform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
