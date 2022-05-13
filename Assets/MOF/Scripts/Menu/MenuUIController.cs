using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    private enum MenuUIPanel 
    { 
        Intro, 
        Campaign, 
        Compendium, 
        Settings
    }

    [SerializeField] private GameObject[] m_Panels;

    private const float PANEL_ANIMATE_DURATION = 0.3f;
    private const float PANEL_ANIMATE_XPOS = 1000f;

    private MenuUIPanel m_CurrentPanel;
    
    
    // Hiding all panels 
    private void Awake()
    {
        for (int i = 1; i < m_Panels.Length; i++)
        {
            m_Panels[i].transform.localPosition = Vector3.zero;
            m_Panels[i].SetActive(false);
        }
    }

    //Button event. Setting active the chosen panel
    public void SetPanel(int panelId)
    {
        CurrentPanel = (MenuUIPanel)panelId;
    }
    
    
    
    //Enabling/disabling and animating the current panel in/out when it is active/inactive
    private MenuUIPanel CurrentPanel
    {
        get
        {
            return m_CurrentPanel;
        }
        set
        {
            GameObject previousPanel = m_Panels[(int)m_CurrentPanel];
            GameObject newPanel = m_Panels[(int)value];
            
            newPanel.SetActive(true);
            AnimatePanelIn(newPanel.GetComponent<CanvasGroup>());
            AnimatePanelOut(previousPanel.GetComponent<CanvasGroup>(), delegate { previousPanel.SetActive(false); });

            m_CurrentPanel = value;
        }
    }
      
    private void AnimatePanelIn(CanvasGroup panel)
    {
        panel.transform.localPosition = new Vector3(PANEL_ANIMATE_XPOS, panel.transform.localPosition.y, panel.transform.localPosition.z);
        panel.transform.DOLocalMoveX(0, PANEL_ANIMATE_DURATION);
        panel.alpha = 0;
        panel.DOFade(1, PANEL_ANIMATE_DURATION);
    }
    private void AnimatePanelOut(CanvasGroup panel, Action onComplete)
    {
        panel.transform.localPosition = new Vector3(0, panel.transform.localPosition.y, panel.transform.localPosition.z);
        panel.transform.DOLocalMoveX(-PANEL_ANIMATE_XPOS, PANEL_ANIMATE_DURATION);
        panel.alpha = 1;
        panel.DOFade(0, PANEL_ANIMATE_DURATION).onComplete = ()=>{ onComplete.Invoke(); };
    }


}
