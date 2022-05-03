using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class AnimatedButton : MonoBehaviour
{
    [SerializeField] private Transform m_Source;

    private Vector3 m_StartScale;
    private const float SCALE_MULTIPLIER = 1.2f;
    private const float SCALE_DURATION = 0.3f;

    private void Awake()
    {
        m_StartScale = m_Source.localScale;
    }

    public void OnHover()
    {
        m_Source.DOScale(m_StartScale * SCALE_MULTIPLIER, SCALE_DURATION);
    }

    public void OnHoverExit()
    {
        m_Source.DOScale(m_StartScale, SCALE_DURATION);
    }
}
