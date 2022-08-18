using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// This class is responsible of all the visual card elements and setting their values
/// </summary>
public class VisualMinion : CharacterLookChanger
{

    public MinionUiPopup minionUiPopup;
    [SerializeField] private GameObject m_NotEnoughManaPopup;

    [Header("Accessories and look")] [SerializeField]
    private GameObject m_SkillfulParticle;

    [SerializeField] private GameObject m_ElementalParticle;
    [SerializeField] private GameObject m_FighterParticle;

    /// <summary>
    /// Spawn partical effects and set their ui texts.
    /// </summary>
    public void VisualizeSpecialPower(bool show, MinionType type, MinionData minionData)
    {
        switch (type)
        {
            case MinionType.Elemental:
                string strengthText = "Strength: ";

                m_ElementalParticle.SetActive(show);
                if (minionData)
                {
                    SetSpecialPowerTMP(strengthText, minionData.strength, 0);
                }

                break;

            case MinionType.Skillful:
                string blockText = "Shield: ";

                m_SkillfulParticle.SetActive(show);
                if (minionData)
                {
                    SetSpecialPowerTMP(blockText, minionData.block, 0);
                }

                break;

            case MinionType.Fighter:
                string attackText = "Attack: ";

                m_FighterParticle.SetActive(show);
                if (minionData)
                {
                    SetSpecialPowerTMP(attackText, minionData.damage, GameManager.Instance.Player.Strength);
                }

                break;

            default:
                m_SkillfulParticle.SetActive(false);
                m_ElementalParticle.SetActive(false);
                m_FighterParticle.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Setting minions cost ui
    /// </summary>
    public void SetMinionCostUI(int cost)
    {
        minionUiPopup.costTMP.text = "Cost: " + cost.ToString();
    }

    /// <summary>
    /// Setting minions title ui
    /// </summary>
    public void SetMinionTitle(string title)
    {
        minionUiPopup.titleTMP.text = "Title: " + title;
    }


    /// <summary>
    /// Setting minions description ui
    /// </summary>
    public void SetMinionDescription(string desc)
    {
        minionUiPopup.descriptionTMP.text = desc;
    }


    /// <summary>
    /// Setting minions power text ui
    /// </summary>
    public void SetSpecialPowerTMP(string text, int damage, int booster)
    {
        if (booster > 0)
        {
            minionUiPopup.boosterTMP.text = " + " + booster;
        }
        else
        {
            minionUiPopup.boosterTMP.text = " ";
        }

        minionUiPopup.specialPowerTMP.text = text + damage;
    }


    /// <summary>
    /// Animate the popup window saying not enough mana
    /// </summary>
    public void NotEnoughManaPopUp()
    {
        m_NotEnoughManaPopup.transform.DOScale(Vector3.one, 0.5f).onComplete = () =>
        {
            m_NotEnoughManaPopup.transform.DOShakeRotation(2, 5).onComplete = () =>
            {
                m_NotEnoughManaPopup.transform.DOScale(Vector3.zero, 1);
            };
        };
    }

}