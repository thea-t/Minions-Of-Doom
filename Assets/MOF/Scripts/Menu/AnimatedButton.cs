using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class animates the menu buttons based on if they are being hovered over
/// </summary>
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

    [UsedImplicitly]
    public void OnHover()
    {
        m_Source.DOScale(m_StartScale * SCALE_MULTIPLIER, SCALE_DURATION);
    }

    [UsedImplicitly]
    public void OnHoverExit()
    {
        m_Source.DOScale(m_StartScale, SCALE_DURATION);
    }
}
