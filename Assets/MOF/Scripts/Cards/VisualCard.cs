using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//This class is responsible of all the visual card elements and setting their values
public class VisualCard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_CostTMP;
    [SerializeField] private TextMeshProUGUI m_TitleTMP;
    [SerializeField] private TextMeshProUGUI m_DescriptionTMP;
    [SerializeField] private Image m_CardImage;
    [SerializeField] private Image m_SkillImage;
    [SerializeField] private Image m_ElementImage;
    [SerializeField] private Image m_MinionImage;

    public void SetCardTypeUI(CardType type)
    {
        switch (type)
        {
            case CardType.Element:
                m_ElementImage.gameObject.SetActive(true);
                break;

            case CardType.Skill:
                m_SkillImage.gameObject.SetActive(true);
                break;

            case CardType.Minion:
                m_MinionImage.gameObject.SetActive(true);
                break;
            
            default:
                Debug.Log("TYPE NOT ASSIGNED");
                break;
        }
    }

    public void SetCardCostUI(int cost)
    {
        m_CostTMP.text = cost.ToString();
    }
    
    public void SetCardTitle(string title)
    {
        m_TitleTMP.text = title;
    }
    
    public void SetCardDescription(string desc)
    {
        m_DescriptionTMP.text = desc;
    }
}
