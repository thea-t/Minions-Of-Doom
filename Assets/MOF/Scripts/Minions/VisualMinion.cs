using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

//This class is responsible of all the visual card elements and setting their values
public class VisualMinion : CharacterLookChanger {

    public MinionUiPopup minionUiPopup;
    
    [Header("Accessories and look")]
    [SerializeField] private GameObject m_SkillfulParticle;
    [SerializeField] private GameObject m_ElementalParticle;
    [SerializeField] private GameObject m_FighterParticle;

    public void SetMinionTypeParticle(MinionType type)
    {
        switch (type)
        {
            case MinionType.Elemental:
                m_ElementalParticle.SetActive(true);
                break;

            case MinionType.Skillful:
                m_SkillfulParticle.SetActive(true);
                break;

            case MinionType.Fighter:
                m_FighterParticle.SetActive(true);
                break;
            
            default:
                Debug.Log("TYPE NOT ASSIGNED");
                break;
        }
    }

    public void SetMinionCostUI(int cost)
    {
        minionUiPopup.costTMP.text = cost.ToString();
    }
    
    public void SetMinionTitle(string title)
    {
        minionUiPopup.titleTMP.text = title;
    }
    
    public void SetMinionDescription(string desc)
    {
        minionUiPopup.descriptionTMP.text = desc;
    }
    
}
